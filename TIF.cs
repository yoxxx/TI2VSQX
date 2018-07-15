using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI2VSQX
{
    public class TIF
    {
        [System.Xml.Serialization.XmlElementAttribute("PhonemeLoc")]
        public int PhonemeLoc { get; set; }                                  //t1:音素位置（0.1マイクロ秒単位)

        [System.Xml.Serialization.XmlElementAttribute("PhonemeLen")]
        public int PhonemeLen { get; set; }                                  //t2:音素長さ（0.1マイクロ秒単位)

        [System.Xml.Serialization.XmlElementAttribute("PhonemeBeforePrev")]
        public string PhonemeBeforePrev { get; set; }                        //p1:音素（２つ前) 

        [System.Xml.Serialization.XmlElementAttribute("PhonemePrev")]
        public string PhonemePrev { get; set; }                              //p2:音素（１つ前) 

        [System.Xml.Serialization.XmlElementAttribute("PhonemeCurrent")]
        public string PhonemeCurrent { get; set; }                           //p3:音素（現在) 

        [System.Xml.Serialization.XmlElementAttribute("PhonemeNext")]
        public string PhonemeNext { get; set; }                              //p4:音素（次) 

        [System.Xml.Serialization.XmlElementAttribute("PhonemeAfterNext")]
        public string PhonemeAfterNext { get; set; }                         //p5:音素（次の次) 

        [System.Xml.Serialization.XmlElementAttribute("MoraDiffAccent")]
        public string MoraDiffAccent { get; set; }                           //a1:アクセント核までのモーラ数（モーラがアクセント核の時ゼロ）

        [System.Xml.Serialization.XmlElementAttribute("MoraPosForward")]
        public string MoraPosForward { get; set; }                           //a2:現在のモーラのアクセント句内での位置（昇順）

        [System.Xml.Serialization.XmlElementAttribute("MoraPosBackward")]
        public string MoraPosBackward { get; set; }                          //a3:現在のモーラのアクセント句内での位置（降順）

        [System.Xml.Serialization.XmlElementAttribute("MoraCurrent")]
        public string MoraCurrent { get; set; }                              //m1:モーラ文字（現在) 

        [System.Xml.Serialization.XmlElementAttribute("POSIDPrev")]
        public string POSIDPrev { get; set; }                                //b1:品詞区分    （前）

        [System.Xml.Serialization.XmlElementAttribute("CFormIDPrev")]
        public string CFormIDPrev { get; set; }                              //b2:活用形区分  （前）

        [System.Xml.Serialization.XmlElementAttribute("CTypeIDPrev")]
        public string CTypeIDPrev { get; set; }                              //b3:活用種類区分（前） 

        [System.Xml.Serialization.XmlElementAttribute("POSIDCurrent")]
        public string POSIDCurrent { get; set; }                             //c1:品詞区分    （現在） 

        [System.Xml.Serialization.XmlElementAttribute("CFormIDCurrent")]
        public string CFormIDCurrent { get; set; }                           //c2:活用形区分  （現在） 

        [System.Xml.Serialization.XmlElementAttribute("CTypeIDCurrent")]
        public string CTypeIDCurrent { get; set; }                           //c3:活用種類区分（現在） 

        [System.Xml.Serialization.XmlElementAttribute("POSIDNext")]
        public string POSIDNext { get; set; }                                //d1:品詞区分    （次） 

        [System.Xml.Serialization.XmlElementAttribute("CFormIDNext")]
        public string CFormIDNext { get; set; }                              //d2:活用形区分  （次） 

        [System.Xml.Serialization.XmlElementAttribute("CTypeIDNext")]
        public string CTypeIDNext { get; set; }                              //d3:活用種類区分（次） 

        [System.Xml.Serialization.XmlElementAttribute("MoraNumPrevAcc")]
        public string MoraNumPrevAcc { get; set; }                           //e1:モーラ数      （前のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("AccTypePrevAcc")]
        public string AccTypePrevAcc { get; set; }                           //e2:アクセント種類（前のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("IsInterrogativePrevAcc")]
        public string IsInterrogativePrevAcc { get; set; }                   //e3:疑問文か      （前のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("UndefinedPrevAcc")]
        public string UndefinedPrevAcc { get; set; }                         //e4:未定義項目    （前のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("IsExistPausePrevAcc")]
        public string IsExistPausePrevAcc { get; set; }                      //e5:ポーズ有無    （前のアクセント句と現在のアクセント句の間） 

        [System.Xml.Serialization.XmlElementAttribute("MoraNumCurrentAcc")]
        public string MoraNumCurrentAcc { get; set; }                        //f1:モーラ数      （現在のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("AccTypeCurrentAcc")]
        public string AccTypeCurrentAcc { get; set; }                        //f2:アクセント種類（現在のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("IsInterrogativeCurrentAcc")]
        public string IsInterrogativeCurrentAcc { get; set; }                //f3:疑問文か      （現在のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("UndefinedCurrentAcc")]
        public string UndefinedCurrentAcc { get; set; }                      //f4:未定義項目    （現在のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("AccPosInBreathForward")]
        public string AccPosInBreathForward { get; set; }                    //f5:現在のアクセント句の呼気段落内での位置（昇順） 

        [System.Xml.Serialization.XmlElementAttribute("AccPosInBreathBackward")]
        public string AccPosInBreathBackward { get; set; }                   //f6:現在のアクセント句の呼気段落内での位置（降順） 

        [System.Xml.Serialization.XmlElementAttribute("FirstAccMoraPosInBreathForward")]
        public string FirstAccMoraPosInBreathForward { get; set; }           //f7:アクセント句内先頭モーラの呼気段落内での位置（昇順） 

        [System.Xml.Serialization.XmlElementAttribute("FirstAccMoraPosInBreathBackward")]
        public string FirstAccMoraPosInBreathBackward { get; set; }          //f8:アクセント句内先頭モーラの呼気段落内での位置（降順） 

        [System.Xml.Serialization.XmlElementAttribute("MoraNumNextAcc")]
        public string MoraNumNextAcc { get; set; }                           //g1:モーラ数      （次のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("AccTypeNextAcc")]
        public string AccTypeNextAcc { get; set; }                           //g2:アクセント種類（次のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("IsInterrogativeNextAcc")]
        public string IsInterrogativeNextAcc { get; set; }                   //g3:疑問文か      （次のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("UndefinedNextAcc")]
        public string UndefinedNextAcc { get; set; }                         //g4:未定義項目    （次のアクセント句） 

        [System.Xml.Serialization.XmlElementAttribute("IsExistPauseNextAcc")]
        public string IsExistPauseNextAcc { get; set; }                      //g5:ポーズ有無    （現在のアクセント句と次のアクセント句の間）

        [System.Xml.Serialization.XmlElementAttribute("AccNumPrevBreath")]
        public string AccNumPrevBreath { get; set; }                         //h1:アクセント句数（前の呼気段落） 

        [System.Xml.Serialization.XmlElementAttribute("MoraNumPrevBreath")]
        public string MoraNumPrevBreath { get; set; }                        //h2:モーラ数      （前の呼気段落）

        [System.Xml.Serialization.XmlElementAttribute("AccNumCurrentBreath")]
        public string AccNumCurrentBreath { get; set; }                      //i1:アクセント句数（現在の呼気段落） 

        [System.Xml.Serialization.XmlElementAttribute("MoraNumCurrentBreath")]
        public string MoraNumCurrentBreath { get; set; }                     //i2:モーラ数      （現在の呼気段落）

        [System.Xml.Serialization.XmlElementAttribute("BreathPosInUtteranceForward")]
        public string BreathPosInUtteranceForward { get; set; }              //i3:現在の呼気段落の発声内での位置（昇順） 

        [System.Xml.Serialization.XmlElementAttribute("BreathPosInUtteranceBackward")]
        public string BreathPosInUtteranceBackward { get; set; }             //i4:現在の呼気段落の発声内での位置（降順） 

        [System.Xml.Serialization.XmlElementAttribute("FirstAccPosInUtteranceForward")]
        public string FirstAccPosInUtteranceForward { get; set; }            //i5:呼気段落内先頭アクセント句の発声内での位置（昇順） 

        [System.Xml.Serialization.XmlElementAttribute("FirstAccPosInUtteranceBackward")]
        public string FirstAccPosInUtteranceBackward { get; set; }           //i6:呼気段落内先頭アクセント句の発声内での位置（降順）

        [System.Xml.Serialization.XmlElementAttribute("FirstBreathMoraPosInUtteranceForward")]
        public string FirstBreathMoraPosInUtteranceForward { get; set; }     //i7:呼気段落内先頭モーラの発声内での位置（昇順）

        [System.Xml.Serialization.XmlElementAttribute("FirstBreathMoraPosInUtteranceBackward")]
        public string FirstBreathMoraPosInUtteranceBackward { get; set; }    //i8:呼気段落内先頭モーラの発声内での位置（降順）

        [System.Xml.Serialization.XmlElementAttribute("AccNumNextBreath")]
        public string AccNumNextBreath { get; set; }                         //j1:アクセント句数（次の呼気段落）

        [System.Xml.Serialization.XmlElementAttribute("MoraNumNextBreath")]
        public string MoraNumNextBreath { get; set; }                        //j2:モーラ数      （次の呼気段落）

        [System.Xml.Serialization.XmlElementAttribute("BreathNumInUtterance")]
        public string BreathNumInUtterance { get; set; }                     //k1:呼気段落数    （現在の発声内） 

        [System.Xml.Serialization.XmlElementAttribute("AccNumInUtterance")]
        public string AccNumInUtterance { get; set; }                        //k2:アクセント句数（現在の発声内） 

        [System.Xml.Serialization.XmlElementAttribute("MoraNumInUtterance")]
        public string MoraNumInUtterance { get; set; }                       //k3:モーラ数      （現在の発声内） 

        public TIF(string Line)
        {
            string[] block = Line.Split('/', ' ');
            this.PhonemeLoc = Int32.Parse(block[0]);
            this.PhonemeLen = Int32.Parse(block[1]);
            string[] item = block[2].Split('^', '-', '+', '=');
            this.PhonemeBeforePrev = item[0];
            this.PhonemePrev = item[1];
            this.PhonemeCurrent = item[2];
            this.PhonemeNext = item[3];
            this.PhonemeAfterNext = item[4];
            item = block[3].Split(':', '+', '=');
            this.MoraDiffAccent = item[1];
            this.MoraPosForward = item[2];
            this.MoraPosBackward = item[3];
            this.MoraCurrent = item[4];
            item = block[4].Split(':', '-', '_');
            this.POSIDPrev = item[1];
            this.CFormIDPrev = item[2];
            this.CTypeIDPrev = item[3];
            item = block[5].Split(':', '_', '+');
            this.POSIDCurrent = item[1];
            this.CFormIDCurrent = item[2];
            this.CTypeIDCurrent = item[3];
            item = block[6].Split(':', '+', '_');
            this.POSIDNext = item[1];
            this.CFormIDNext = item[2];
            this.CTypeIDNext = item[3];
            item = block[7].Split(':', '_', '!', '-');
            this.MoraNumPrevAcc = item[1];
            this.AccTypePrevAcc = item[2];
            this.IsInterrogativePrevAcc = item[3];
            this.UndefinedPrevAcc = item[4];
            this.IsExistPausePrevAcc = item[5];
            item = block[8].Split(':', '_', '#', '@', '|');
            this.MoraNumCurrentAcc = item[1];
            this.AccTypeCurrentAcc = item[2];
            this.IsInterrogativeCurrentAcc = item[3];
            this.UndefinedCurrentAcc = item[4];
            this.AccPosInBreathForward = item[5];
            this.AccPosInBreathBackward = item[6];
            this.FirstAccMoraPosInBreathForward = item[7];
            this.FirstAccMoraPosInBreathBackward = item[8];
            item = block[9].Split(':', '_', '%');
            this.MoraNumNextAcc = item[1];
            this.AccTypeNextAcc = item[2];
            this.IsInterrogativeNextAcc = item[3];
            this.UndefinedNextAcc = item[4];
            this.IsExistPauseNextAcc = item[5];
            item = block[10].Split(':', '_');
            this.AccNumPrevBreath = item[1];
            this.MoraNumPrevBreath = item[2];
            item = block[11].Split(':', '-', '@', '+', '&', '|');
            this.AccNumCurrentBreath = item[1];
            this.MoraNumCurrentBreath = item[2];
            this.BreathPosInUtteranceForward = item[3];
            this.BreathPosInUtteranceBackward = item[4];
            this.FirstAccPosInUtteranceForward = item[5];
            this.FirstAccPosInUtteranceBackward = item[6];
            this.FirstBreathMoraPosInUtteranceForward = item[7];
            this.FirstBreathMoraPosInUtteranceBackward = item[8];
            item = block[12].Split(':', '_');
            this.AccNumNextBreath = item[1];
            this.MoraNumNextBreath = item[2];
            item = block[13].Split(':', '+', '-');
            this.BreathNumInUtterance = item[1];
            this.AccNumInUtterance = item[2];
            this.MoraNumInUtterance = item[3];
        }

        public TIF() { }

    }

}
