using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace ContourAnalysisNS
{
    public class Backup
    {
        private int tempY, nOfM, nOfChoices, xDiff, masterDiff, minX, maxX,yPad;
        private Dictionary<int ,List<Point>> dict = new Dictionary<int,List<Point>>();
        private List<Point> list = new List<Point>();
        private List<Point> tempList = new List<Point>();
        private List<NumericUpDown> nQuestionsPerColumnList = new List<NumericUpDown>();
        private List<int> minXs = new List<int>();
        private List<int> maxXs = new List<int>();
        private List<int> startKeyOfMasters = new List<int>();
        private bool needsCorrection;
        public bool isFirstImage = false;
        int keyCount = 1;
        public void setNOfM(int n)
        {
            nOfM = n;
        }
        public int getNOfM()
        {
            return nOfM;
        }
        public void setNOfChoice(int n)
        {
            nOfChoices = n;
        }
        public int getNOfChoices()
        {
            return nOfChoices;
        }
        public void setQperColumnList(List<NumericUpDown> list)
        {
            nQuestionsPerColumnList = list;
        }
        public List<NumericUpDown> getQperColumnList()
        {
            return nQuestionsPerColumnList;
        }

        public List<Point> fixList(List<Point> L)
        {
            
            setMin(L);  //min value in X (mish point)
            addToDictionaryByColumn(L);
            
            //checking if list doesn't need backup
            var i = dict.Values.Sum(x => x.Count);
            if (i == nOfChoices * nQuestionsPerColumnList.Sum(x => x.Value))
            {
                //generate missing circles
                foreach (var question in dict)
                {
                    if (question.Value.Count != 0)
                    {
                        quickSortX(question.Value, 0, question.Value.Count - 1);
                    }
                }
                addValuesToList(L, dict, 1);
                return L;
            }
            correctMissing();
            /*foreach(var item in dict)
            {
                Console.WriteLine("Key : {0}        Count : {1}", item.Key, item.Value.Count);
                for (int k = 0; k < item.Value.Count; k++)
                    Console.Write("      {0}",item.Value[k]);
                Console.WriteLine("");
                
            }*/
            //clear list
            L.Clear();

            //copy the fixed dictionary to the list
            addValuesToList(L, dict, 1);

            //return fixed list
            return L;
        }
        private void setXMin2(List<Point> list)
        {
            List<Point> temp = new List<Point>();
            List<int> temp2 = new List<int>();
            int Y = list[0].Y;
            for (int i = 0; i < list.Count; i++)
            {
                if (Math.Abs(list[i].Y - Y) <=5)
                {
                    temp.Add(list[i]);
                    temp.Add(list[i + 1]);
                }
                if (temp.Count > nOfChoices)
                {
                    break;
                }
            }
            for (int j = 0; j < temp.Count; j++)
            {
                if(j+1 != temp.Count)
                    if(Math.Abs(temp[j+1].X - temp[j].X) >20)
                        temp2.Add(Math.Abs(temp[j+1].X - temp[j].X));
            }
            xDiff = temp2.Min();
        }
        private void setYPad(List<Point> list)
        {
            int Y = list[0].Y;
            for (int i = 0; i < list.Count; i++)
            {
                if (Math.Abs(list[i].Y - Y) > 20)
                {
                    yPad = Math.Abs(list[i].Y - Y);
                    break;
                }
            }
        }
        private void addToDictionaryByColumn(List<Point> L)
        {
            int removeCounter = 0;
            if (isFirstImage == true)
            {
                setXMin2(L);
                setYPad(L);
            }
            for (int i = 0; i < nOfM; i++)
            {
                foreach (var item in L)
                {
                    if (item.X >= minX && item.X <= minX + (nOfChoices * xDiff) +20)
                    {
                        tempList.Add(item);
                        removeCounter++;
                    }
                }

                if (tempList.Count == 0)
                {
                    return;
                }
                setMax(tempList);
                maxXs.Add(maxX);
                startKeyOfMasters.Add(keyCount);
                for (int j = 0; j < tempList.Count; j++)
                {
                    if (j + 1 != tempList.Count)
                    {
                        if(Math.Abs(tempList[j+1].Y - tempList[j].Y) > 10 && Math.Abs(tempList[j+1].Y - tempList[j].Y) < (yPad - 5))
                             tempList.RemoveAt(j+1);
                    }
                }
                addValuesToDictionary(dict, tempList, tempList[0].Y, keyCount++);
                quickSortX(L, 0, L.Count - 1);
                for (int z = 0; z < removeCounter; z++)
                {
                    if (0 < L.Count)   //kill ma khallis row, faddeele L points li kamashton mnil list L aseseyye. Bi ma inna sorted,fa akeed L points li 3tabaron nafs L Y w zatton bil dictionary mawjoodeen bil List, w 3adadon 3adad L removeCounter. W matra7on dayman 7a yballish mnil L[0] li2an 3am nimshe bil tirteeb
                        L.RemoveAt(0);
                }
                removeCounter = 0;
                L.Sort(new PointComparer());
                minXs.Add(minX);
                if(L.Count != 0)
                    setMin(L); 
            }
        }
        private void setMin(List<Point> L)
        {
            int min=L[0].X; //initial value, ma khassa bil final value of minX
            foreach (var item in L)
            {
                if (item.X < min)   //iterate through list la tle2e 2a2alla X
                    min = item.X;   
            }
            minX = min; //zetta bil minX
        }
        private void setMax(List<Point> L)
        {
            int max = L[L.Count - 1].X;   //initial value, ma khassa bil final value of maxX
            foreach (var item in L)
            {
                if (item.X > max)   //iterate through list la tle2e 2a3la X
                    max = item.X;
            }
            maxX = max; //zetta bil maxX
        }
        private void addValuesToList(List<Point> L, Dictionary<int,List<Point>> dict, int key)
        {
            foreach (var item in dict[key])
            {
                L.Add(item);
            }
            if (dict.Count !=(L.Count/(nOfChoices)))   //Dall 7ott items la yseer 3adad L list mazboot
            {                                               //feek ta3mila bi for loop w bala hal labake killa. for int = 1 i <= 3adad L keys L.Add L points li bil list li bil key
                key++;
                addValuesToList(L, dict, key);//recursion
            }
        }
        private void addValuesToDictionary(Dictionary<int, List<Point>> dict, List<Point> L, int oldY, int k)
        {
            tempY = oldY;   //tempY heyye L value of Y bi kill row, initially L Y taba3 awwal row
            List<Point> temp = new List<Point>();   //L list li 3am 7ott feya kill L points li bi a specific row
            int removeCounter = 0;  //heda mista3mlo L n0oB la yfadde mnil list L2aseseyye bas yi5las min kill row

            foreach (var item in L)
            {
                if (!yChanged(item.Y))  //shifle iza hal item of L 3indo nafs L Y taba3 L tempY(L tempY heyye value of Y la kill row, initially bi awwal row)
                {
                    temp.Add(item); //iza 2e zittille hal Point bil list, manna yeha li2anna bil row
                    removeCounter++;    //heda la y3eddelle kamm point 3indon nafs L Y feek t2ool. Ya3ne ra7 2ista3mlo mitel ma 2ilna la nsheel items mnil List kill ma 2i5las row, tzakkar inno L list L 5arbene jeye sorted
                }
                else
                break;
            }
            dict.Add(k, temp);  //zett L list bil corresponding key. Ya3ne 3am nimshe feyon key wara key bil terteeb. Kill key 3am n7ott fee L points li 3indon nafs L Y li lezim ykoono fee

            //temp.Clear(); //L ja7sh 3am yi5la2a kill marra w jey ya3mella clear hahahaha
            for (int i = 0; i < removeCounter; i++)
            {
                if(0<L.Count)   //kill ma khallis row, faddeele L points li kamashton mnil list L aseseyye. Bi ma inna sorted,fa akeed L points li 3tabaron nafs L Y w zatton bil dictionary mawjoodeen bil List, w 3adadon 3adad L removeCounter. W matra7on dayman 7a yballish mnil L[0] li2an 3am nimshe bil tirteeb
                L.RemoveAt(0);
            }
            if (L.Count>0)  //recursion inno dall zett bil dictionary la ti5las L list 
            {
                k++;
                keyCount++;
                addValuesToDictionary(dict, L, L[0].Y, k);//recursion
            }
            removeCounter = 0;
        }
        private bool yChanged(int newValue)
        {
            if (Math.Abs(newValue - tempY) < 10)
                return false;
            else return true;
        }
        void quickSortX(List<Point> arr, int left, int right)   //malnash da3wa...bas inno divide and conquer
        {
            int i = left, j = right;
            Point tmp;
            Point pivot = arr[(left + right) / 2];

            /* partition */

            while (i <= j)
            {
                while (arr[i].X < pivot.X)
                    i++;
                while (arr[j].X > pivot.X)
                    j--;
                if (i <= j)
                {
                    tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;
                    i++;
                    j--;
                }
            };

            /* recursion */

            if (left < j)
                quickSortX(arr, left, j);
            if (i < right)
                quickSortX(arr, i, right);
        }
        private void setXDiff()
        {
            foreach (var item in dict)  //torawidoni Lshokook....
            {
                if (item.Value.Count == nOfChoices)
                {
                    quickSortX(item.Value, 0, (nOfChoices) - 1);
                    xDiff = item.Value[1].X - item.Value[0].X;
                    break;
                }
            }
        }
        private void correctMissing()
        {
            //set xDiff         //7LOF !
            int masterIndex = 0;
            int questionCount = 0;
            int nQuestions = (int)nQuestionsPerColumnList[0].Value;
            int nMasterColumns = nQuestionsPerColumnList.Count;
            if (dict.Count != 0)
            {
                foreach (var question in dict)
                {
                    if (question.Value.Count != 0)
                    {
                        quickSortX(question.Value, 0, question.Value.Count - 1);
                        if (!exists(minXs[masterIndex], question.Value))
                        {
                            getMissing(1, question.Key, masterIndex);
                            quickSortX(question.Value, 0, question.Value.Count - 1);
                        }
                        if (!exists(maxXs[masterIndex], question.Value))
                        {
                            getMissing(2, question.Key, masterIndex);
                            quickSortX(question.Value, 0, question.Value.Count - 1);
                        }
                        if (!exists(question.Value[0].X + xDiff, question.Value))
                        {
                            getMissing(3, question.Key, masterIndex);
                            quickSortX(question.Value, 0, question.Value.Count - 1);
                        }
                        if (!exists(question.Value[question.Value.Count - 1].X - xDiff, question.Value))
                        {
                            getMissing(4, question.Key, masterIndex);
                            quickSortX(question.Value, 0, question.Value.Count - 1);
                        }
                        if (!exists(question.Value[1].X + xDiff, question.Value))
                        {
                            getMissing(5, question.Key, masterIndex);
                            quickSortX(question.Value, 0, question.Value.Count - 1);
                        }
                    }
                    questionCount++;
                    if (questionCount == nQuestions)
                    {
                        masterIndex++;
                        if (masterIndex == nMasterColumns)
                            return;
                        nQuestions = nQuestions + (int)nQuestionsPerColumnList[masterIndex].Value;
                    }
                }
            }
        }
        private bool exists(int n , List<Point> L)
        {
            foreach (var item in L) //shifle iza hal value bil X mawjood bil circle. Bass.
            {
                if (Math.Abs(n - item.X) < 35)//accuracy of 20px
                    return true;
            }
            return false;
        }
        private int getMissingIdX(List<Point> L)
        {
            int maxIdx = L.Count - 1;   //maximum index momkin ykoon 2e5er item bil list
            quickSortX(L, 0, L.Count - 1);//make sure it is sorted with respect to Xs   //le mish wese2 bi shighlak????
            for (int i = 0; i <= maxIdx; i++)  //فلنبدأ رحلة البحث عن الميسينغ إندكس
            {
                if (Math.Abs(L[i].X - L[i + 1].X) > xDiff+10 && Math.Abs(L[i].X - L[i + 1].X) < masterDiff-10)
                //Iza L fare2 ben L Xs of two items bil list akbar mnil xDiff ma3 7abbet error w kamen azghar mnil masterDiff ma3neta fee circle bayneton day3a
                    return i + 1;
                else if (Math.Abs(L[i].X - L[i + 1].X) > xDiff + masterDiff - 10)   //hay mish m3ajbeetne....
                    return i + 1;
            }
            return -1;//if index was not found
        }
        private void getMissing(int c, int key, int masterIndex)
        {
            int x, y;   //Ho 7a ykoono L values lal no2ta L day3a yabne....
            switch (c)
            {
                case 1: //iza awwal circle day3a
                    if (existsInStartKeyList(key))
                    {
                        x = minXs[masterIndex];
                        //x = dict[key][0].X - xDiff; //hon awwal item bil list 7a ykoon bil we2i3 TENE circle. Fa la ntalle3 L coordinates la awwal cirlce minna2is L X te3ool tene dowwayra bil xDifference
                    }
                    else
                    {
                        x = dict[key - 1][0].X;
                    }
                    y = dict[key][0].Y; //w mnishkilla nafs L Y taba3 tene circle
                    dict[key].Insert(0, new Point(x, y));   //zeed L no2ta ltalla3neha bi awwal index mnil List li 3am nishtighil 3laya
                    break;

                case 2: //iza 2e5er circle day3a
                    if (existsInStartKeyList(key))
                    {
                        x = maxXs[masterIndex];
                        //x = dict[key][(nOfChoices) - 2].X + xDiff; //3teya X L X lal circle li abla + xDiff
                    }
                    else
                    {
                        x = dict[key - 1][(nOfChoices) - 1].X;
                    }
                    y = dict[key][0].Y; //w shkilla nafs L Y
                    dict[key].Add(new Point(x, y)); //zeed L no2ta ltalla3neha bi 2e5er L List li 3am nishtighil 3laya
                     break;

                case 3: //iza tene no2ta day3a
                     if (existsInStartKeyList(key))
                     {
                         x = minXs[masterIndex] + xDiff;
                     }
                     else
                     {
                         x = (minXs[masterIndex] + xDiff + dict[key - 1][1].X) / 2;
                     }
                    y = dict[key][0].Y;
                    dict[key].Insert(1, new Point(x, y));
                     break;
                case 4: //iza abel 2e5er no2ta day3a
                     if (existsInStartKeyList(key))
                     {
                         x = maxXs[masterIndex] - xDiff;
                     }
                     else
                     {
                         x = dict[key - 1][nOfChoices - 2].X;
                     }
                     y = dict[key][0].Y;
                    dict[key].Insert(nOfChoices - 2, new Point(x, y));
                     break;
                case 5: //iza abel 2e5er no2ta day3a
                     if (existsInStartKeyList(key))
                     {
                         x = dict[key][1].X + xDiff;
                     }
                     else
                     {
                         x = dict[key - 1][2].X;
                     }
                     y = dict[key][nOfChoices - 2].Y;
                    dict[key].Insert(2, new Point(x, y));
                     break;
            }
        }
        private bool existsInStartKeyList(int key)
        {
            int i = 0;
            while (i < startKeyOfMasters.Count)
            {
                if (key == startKeyOfMasters[i])
                    return true;
                else
                    i++;
            }
            return false;
        }
        public int getXdiff()
        {
            return xDiff;
        }
        public int getYpad()
        {
            return yPad;
        }
        public void setFirstImageTrue()
        {
            isFirstImage = true;
        }
        public void setXdiff(int x)
        {
            xDiff = x;
        }
        public void setYpad(int y)
        {
            yPad = y;
        }
    }
}