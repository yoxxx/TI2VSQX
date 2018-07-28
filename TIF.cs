/*
 * TIF.cs
 * Copyright c 2018 yo_xxx
 *
 * This file is part of TI2VSQX.
 *
 * TI2VSQX is free software; you can redistribute it and/or
 * modify it under the terms of the BSD License.
 *
 * TI2VSQX is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace TI2VSQX
{
    /// <summary>
    /// トレース情報のテキスト解析情報（[Text analysis result]）のクラス
    /// </summary>
    public class TIF_TextAnalysis
    {
        /// <summary>
        /// 解析元文字列
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("StrAnalysis")]
        public string StrAnalysis { get; set; }     //解析元文字列

        /// <summary>
        /// 品詞
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("POS")]
        public string POS { get; set; }             //品詞

        /// <summary>
        /// 品詞分類１
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("POS_group1")]
        public string POS_group1 { get; set; }      //品詞分類１

        /// <summary>
        /// 品詞分類２
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("POS_group2")]
        public string POS_group2 { get; set; }      //品詞分類２

        /// <summary>
        /// 品詞分類３
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("POS_group3")]
        public string POS_group3 { get; set; }      //品詞分類３

        /// <summary>
        /// 活用種類
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CType")]
        public string CType { get; set; }           //活用種類

        /// <summary>
        /// 活用形
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CForm")]
        public string CForm { get; set; }           //活用形

        /// <summary>
        /// 解析後文字列（辞書になければ*、あれば辞書の単語）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("StrOriginal")]
        public string StrOriginal { get; set; }     //解析後文字列（辞書になければ*、あれば辞書の単語）

        /// <summary>
        /// 解析後文字列の読み
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("StrRead")]
        public string StrRead { get; set; }         //解析後文字列の読み

        /// <summary>
        /// 解析後文字列の読みを発声に合わせて直したもの
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("StrPhoneme")]
        public string StrPhoneme { get; set; }      //解析後文字列の読みを発声に合わせて直したもの

        /// <summary>
        /// アクセント区分
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AccID")]
        public string AccID { get; set; }           //アクセント区分

        /// <summary>
        /// モーラ数
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraSize")]
        public string MoraSize { get; set; }        //モーラ数

        /// <summary>
        /// 連結規則
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("ChainRule")]
        public string ChainRule { get; set; }       //連結規則

        /// <summary>
        /// 連結区分
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("ChainFlag")]
        public string ChainFlag { get; set; }       //連結区分

        //コンストラクタ
        /// <summary>
        /// トレース情報のテキスト解析情報（[Text analysis result]）のクラスのコンストラクタ
        /// </summary>
        /// <param name="Line">テキスト解析情報の行</param>
        /// <returns>トレース情報のテキスト解析情報クラス</returns>
        public TIF_TextAnalysis(string Line)
        {
            string[] item = Line.Split(',');
            StrAnalysis = item[00];
            POS         = item[01];
            POS_group1  = item[02];
            POS_group2  = item[03];
            POS_group3  = item[04];
            CType       = item[05];
            CForm       = item[06];
            StrOriginal = item[07];
            StrRead     = item[08];
            StrPhoneme  = item[09];
            string[] Num = item[10].Split('/');
            AccID       = Num[00];
            MoraSize    = Num[01];
            ChainRule   = item[11];
            ChainFlag   = item[12];
        }

        /// <summary>
        /// シリアライズできるようにパラメータを持たないコンストラクタを用意する
        /// </summary>
        public TIF_TextAnalysis() { }

    }

    /// <summary>
    /// トレース情報の出力ラベル情報（[Output label]）のクラス
    /// </summary>
    public class TIF
    {
        /// <summary>
        /// t1:音素開始位置（0.1マイクロ秒単位)
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("PhonemeFrom")]
        public int PhonemeFrom { get; set; }                                 //t1:音素開始位置（0.1マイクロ秒単位)

        /// <summary>
        /// t2:音素終了位置（0.1マイクロ秒単位)
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("PhonemeTo")]
        public int PhonemeTo { get; set; }                                   //t2:音素終了位置（0.1マイクロ秒単位)

        /// <summary>
        /// p1:音素（２つ前) 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("PhonemeBeforePrev")]
        public string PhonemeBeforePrev { get; set; }                        //p1:音素（２つ前) 

        /// <summary>
        /// p2:音素（１つ前) 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("PhonemePrev")]
        public string PhonemePrev { get; set; }                              //p2:音素（１つ前) 

        /// <summary>
        /// p3:音素（現在) 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("PhonemeCurrent")]
        public string PhonemeCurrent { get; set; }                           //p3:音素（現在) 

        /// <summary>
        /// p4:音素（次) 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("PhonemeNext")]
        public string PhonemeNext { get; set; }                              //p4:音素（次) 

        /// <summary>
        /// p5:音素（次の次) 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("PhonemeAfterNext")]
        public string PhonemeAfterNext { get; set; }                         //p5:音素（次の次) 

        /// <summary>
        /// a1:アクセント核までのモーラ数（モーラがアクセント核の時ゼロ）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraDiffAccent")]
        public string MoraDiffAccent { get; set; }                           //a1:アクセント核までのモーラ数（モーラがアクセント核の時ゼロ）

        /// <summary>
        /// a2:現在のモーラのアクセント句内での位置（昇順）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraPosForward")]
        public string MoraPosForward { get; set; }                           //a2:現在のモーラのアクセント句内での位置（昇順）

        /// <summary>
        /// a3:現在のモーラのアクセント句内での位置（降順）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraPosBackward")]
        public string MoraPosBackward { get; set; }                          //a3:現在のモーラのアクセント句内での位置（降順）

        /// <summary>
        /// a4:モーラ文字（現在) ※Open JTalkのjpcommon_label.cを修正して出力している
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraCurrent")]
        public string MoraCurrent { get; set; }                              //a4:モーラ文字（現在) ※Open JTalkのjpcommon_label.cを修正して出力している

        /// <summary>
        /// b1:品詞区分    （前）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("POSIDPrev")]
        public string POSIDPrev { get; set; }                                //b1:品詞区分    （前）

        /// <summary>
        /// b1:品詞名      （前）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("POSNamePrev")]
        public string POSNamePrev { get; set; }                              //b1:品詞名      （前）

        /// <summary>
        /// b2:活用形区分  （前）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CFormIDPrev")]
        public string CFormIDPrev { get; set; }                              //b2:活用形区分  （前）

        /// <summary>
        /// b2:活用形名    （前）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CFormNamePrev")]
        public string CFormNamePrev { get; set; }                            //b2:活用形名    （前）

        /// <summary>
        /// b3:活用種類区分（前） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CTypeIDPrev")]
        public string CTypeIDPrev { get; set; }                              //b3:活用種類区分（前） 

        /// <summary>
        /// b3:活用種類名  （前） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CTypeNamePrev")]
        public string CTypeNamePrev { get; set; }                            //b3:活用種類名  （前） 

        /// <summary>
        /// c1:品詞区分    （現在） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("POSIDCurrent")]
        public string POSIDCurrent { get; set; }                             //c1:品詞区分    （現在） 

        /// <summary>
        /// c1:品詞名      （現在）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("POSNameCurrent")]
        public string POSNameCurrent { get; set; }                           //c1:品詞名      （現在）

        /// <summary>
        /// c2:活用形区分  （現在） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CFormIDCurrent")]
        public string CFormIDCurrent { get; set; }                           //c2:活用形区分  （現在） 

        /// <summary>
        /// c2:活用形名    （現在）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CFormNameCurrent")]
        public string CFormNameCurrent { get; set; }                         //c2:活用形名    （現在）

        /// <summary>
        /// c3:活用種類区分（現在） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CTypeIDCurrent")]
        public string CTypeIDCurrent { get; set; }                           //c3:活用種類区分（現在） 

        /// <summary>
        /// c3:活用種類名  （現在） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CTypeNameCurrent")]
        public string CTypeNameCurrent { get; set; }                         //c3:活用種類名  （現在） 

        /// <summary>
        /// d1:品詞区分    （次） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("POSIDNext")]
        public string POSIDNext { get; set; }                                //d1:品詞区分    （次） 

        /// <summary>
        /// d1:品詞名      （次）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("POSNameNext")]
        public string POSNameNext { get; set; }                              //d1:品詞名      （次）

        /// <summary>
        /// d2:活用形区分  （次） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CFormIDNext")]
        public string CFormIDNext { get; set; }                              //d2:活用形区分  （次） 

        /// <summary>
        /// d2:活用形名    （次）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CFormNameNext")]
        public string CFormNameNext { get; set; }                            //d2:活用形名    （次）

        /// <summary>
        /// d3:活用種類区分（次） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CTypeIDNext")]
        public string CTypeIDNext { get; set; }                              //d3:活用種類区分（次） 

        /// <summary>
        /// d3:活用種類名  （次） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CTypeNameNext")]
        public string CTypeNameNext { get; set; }                            //d3:活用種類名  （次） 

        /// <summary>
        /// e1:モーラ数      （前のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraNumPrevAcc")]
        public string MoraNumPrevAcc { get; set; }                           //e1:モーラ数      （前のアクセント句） 

        /// <summary>
        /// e2:アクセント種類（前のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AccTypePrevAcc")]
        public string AccTypePrevAcc { get; set; }                           //e2:アクセント種類（前のアクセント句） 

        /// <summary>
        /// e3:疑問文か      （前のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("IsInterrogativePrevAcc")]
        public string IsInterrogativePrevAcc { get; set; }                   //e3:疑問文か      （前のアクセント句） 

        /// <summary>
        /// e4:未定義項目    （前のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("UndefinedPrevAcc")]
        public string UndefinedPrevAcc { get; set; }                         //e4:未定義項目    （前のアクセント句） 

        /// <summary>
        /// e5:ポーズ有無    （前のアクセント句と現在のアクセント句の間） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("IsExistPausePrevAcc")]
        public string IsExistPausePrevAcc { get; set; }                      //e5:ポーズ有無    （前のアクセント句と現在のアクセント句の間） 

        /// <summary>
        /// f1:モーラ数      （現在のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraNumCurrentAcc")]
        public string MoraNumCurrentAcc { get; set; }                        //f1:モーラ数      （現在のアクセント句） 

        /// <summary>
        /// f2:アクセント種類（現在のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AccTypeCurrentAcc")]
        public string AccTypeCurrentAcc { get; set; }                        //f2:アクセント種類（現在のアクセント句） 

        /// <summary>
        /// f3:疑問文か      （現在のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("IsInterrogativeCurrentAcc")]
        public string IsInterrogativeCurrentAcc { get; set; }                //f3:疑問文か      （現在のアクセント句） 

        /// <summary>
        /// f4:未定義項目    （現在のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("UndefinedCurrentAcc")]
        public string UndefinedCurrentAcc { get; set; }                      //f4:未定義項目    （現在のアクセント句） 

        /// <summary>
        /// f5:現在のアクセント句の呼気段落内での位置（昇順） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AccPosInBreathForward")]
        public string AccPosInBreathForward { get; set; }                    //f5:現在のアクセント句の呼気段落内での位置（昇順） 

        /// <summary>
        /// f6:現在のアクセント句の呼気段落内での位置（降順） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AccPosInBreathBackward")]
        public string AccPosInBreathBackward { get; set; }                   //f6:現在のアクセント句の呼気段落内での位置（降順） 

        /// <summary>
        /// f7:アクセント句内先頭モーラの呼気段落内での位置（昇順） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("FirstAccMoraPosInBreathForward")]
        public string FirstAccMoraPosInBreathForward { get; set; }           //f7:アクセント句内先頭モーラの呼気段落内での位置（昇順） 

        /// <summary>
        /// f8:アクセント句内先頭モーラの呼気段落内での位置（降順） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("FirstAccMoraPosInBreathBackward")]
        public string FirstAccMoraPosInBreathBackward { get; set; }          //f8:アクセント句内先頭モーラの呼気段落内での位置（降順） 

        /// <summary>
        /// g1:モーラ数      （次のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraNumNextAcc")]
        public string MoraNumNextAcc { get; set; }                           //g1:モーラ数      （次のアクセント句） 

        /// <summary>
        /// g2:アクセント種類（次のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AccTypeNextAcc")]
        public string AccTypeNextAcc { get; set; }                           //g2:アクセント種類（次のアクセント句） 

        /// <summary>
        /// g3:疑問文か      （次のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("IsInterrogativeNextAcc")]
        public string IsInterrogativeNextAcc { get; set; }                   //g3:疑問文か      （次のアクセント句） 

        /// <summary>
        /// g4:未定義項目    （次のアクセント句） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("UndefinedNextAcc")]
        public string UndefinedNextAcc { get; set; }                         //g4:未定義項目    （次のアクセント句） 

        /// <summary>
        /// g5:ポーズ有無    （現在のアクセント句と次のアクセント句の間）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("IsExistPauseNextAcc")]
        public string IsExistPauseNextAcc { get; set; }                      //g5:ポーズ有無    （現在のアクセント句と次のアクセント句の間）

        /// <summary>
        /// h1:アクセント句数（前の呼気段落） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AccNumPrevBreath")]
        public string AccNumPrevBreath { get; set; }                         //h1:アクセント句数（前の呼気段落） 

        /// <summary>
        /// h2:モーラ数      （前の呼気段落）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraNumPrevBreath")]
        public string MoraNumPrevBreath { get; set; }                        //h2:モーラ数      （前の呼気段落）

        /// <summary>
        /// i1:アクセント句数（現在の呼気段落） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AccNumCurrentBreath")]
        public string AccNumCurrentBreath { get; set; }                      //i1:アクセント句数（現在の呼気段落） 

        /// <summary>
        /// i2:モーラ数      （現在の呼気段落）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraNumCurrentBreath")]
        public string MoraNumCurrentBreath { get; set; }                     //i2:モーラ数      （現在の呼気段落）

        /// <summary>
        /// i3:現在の呼気段落の発声内での位置（昇順） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("BreathPosInUtteranceForward")]
        public string BreathPosInUtteranceForward { get; set; }              //i3:現在の呼気段落の発声内での位置（昇順） 

        /// <summary>
        /// i4:現在の呼気段落の発声内での位置（降順） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("BreathPosInUtteranceBackward")]
        public string BreathPosInUtteranceBackward { get; set; }             //i4:現在の呼気段落の発声内での位置（降順） 

        /// <summary>
        /// i5:呼気段落内先頭アクセント句の発声内での位置（昇順） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("FirstAccPosInUtteranceForward")]
        public string FirstAccPosInUtteranceForward { get; set; }            //i5:呼気段落内先頭アクセント句の発声内での位置（昇順） 

        /// <summary>
        /// i6:呼気段落内先頭アクセント句の発声内での位置（降順）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("FirstAccPosInUtteranceBackward")]
        public string FirstAccPosInUtteranceBackward { get; set; }           //i6:呼気段落内先頭アクセント句の発声内での位置（降順）

        /// <summary>
        /// i7:呼気段落内先頭モーラの発声内での位置（昇順）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("FirstBreathMoraPosInUtteranceForward")]
        public string FirstBreathMoraPosInUtteranceForward { get; set; }     //i7:呼気段落内先頭モーラの発声内での位置（昇順）

        /// <summary>
        /// i8:呼気段落内先頭モーラの発声内での位置（降順）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("FirstBreathMoraPosInUtteranceBackward")]
        public string FirstBreathMoraPosInUtteranceBackward { get; set; }    //i8:呼気段落内先頭モーラの発声内での位置（降順）

        /// <summary>
        /// j1:アクセント句数（次の呼気段落）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AccNumNextBreath")]
        public string AccNumNextBreath { get; set; }                         //j1:アクセント句数（次の呼気段落）

        /// <summary>
        /// j2:モーラ数      （次の呼気段落）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraNumNextBreath")]
        public string MoraNumNextBreath { get; set; }                        //j2:モーラ数      （次の呼気段落）

        /// <summary>
        /// k1:呼気段落数    （現在の発声内）
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("BreathNumInUtterance")]
        public string BreathNumInUtterance { get; set; }                     //k1:呼気段落数    （現在の発声内） 

        /// <summary>
        /// k2:アクセント句数（現在の発声内） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AccNumInUtterance")]
        public string AccNumInUtterance { get; set; }                        //k2:アクセント句数（現在の発声内） 

        /// <summary>
        /// k3:モーラ数      （現在の発声内） 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MoraNumInUtterance")]
        public string MoraNumInUtterance { get; set; }                       //k3:モーラ数      （現在の発声内） 

        /// <summary>
        /// 品詞区分とその名称
        /// </summary>
        public struct POSIDName
        {
            public string POSID;
            public string POSName;
        };

        /// <summary>
        /// 品詞区分とその名称のテーブル
        /// </summary>
        static public POSIDName[] POSIDNameArray = new POSIDName[]
        {
            new POSIDName { POSID = "xx", POSName = "その他" },
            new POSIDName { POSID = "09", POSName = "感動詞" },
            new POSIDName { POSID = "19", POSName = "形状詞" },
            new POSIDName { POSID = "01", POSName = "形容詞" },
            new POSIDName { POSID = "23", POSName = "助詞-その他" },
            new POSIDName { POSID = "13", POSName = "助詞-格助詞" },
            new POSIDName { POSID = "24", POSName = "助詞-係助詞" },
            new POSIDName { POSID = "14", POSName = "助詞-終助詞" },
            new POSIDName { POSID = "12", POSName = "助詞-接続助詞" },
            new POSIDName { POSID = "11", POSName = "助詞-副助詞" },
            new POSIDName { POSID = "10", POSName = "助動詞" },
            new POSIDName { POSID = "08", POSName = "接続詞" },
            new POSIDName { POSID = "16", POSName = "接頭辞" },
            new POSIDName { POSID = "15", POSName = "接尾辞" },
            new POSIDName { POSID = "04", POSName = "代名詞" },
            new POSIDName { POSID = "20", POSName = "動詞" },
            new POSIDName { POSID = "17", POSName = "動詞-非自立" },
            new POSIDName { POSID = "06", POSName = "副詞" },
            new POSIDName { POSID = "03", POSName = "名詞-サ変接続" },
            new POSIDName { POSID = "18", POSName = "名詞-固有名詞" },
            new POSIDName { POSID = "05", POSName = "名詞-数詞" },
            new POSIDName { POSID = "22", POSName = "名詞-非自立" },
            new POSIDName { POSID = "02", POSName = "名詞-普通名詞" },
            new POSIDName { POSID = "07", POSName = "連体詞" },
            new POSIDName { POSID = "25", POSName = "フィラー" }
        };

        /// <summary>
        /// 品詞区分の名称を求める
        /// </summary>
        static public string GetPOSName(string POSIDValue)
        {
            for (int i = 0; i < POSIDNameArray.Length; i++)
            {
                if (POSIDValue == POSIDNameArray[i].POSID)
                {
                    return POSIDNameArray[i].POSName;
                }
            };
            //見つからなければ空文字列を返す
            return "";
        }

        /// <summary>
        /// 活用形区分とその名称
        /// </summary>
        public struct CFormIDName
        {
            public string CFormID;
            public string CFormName;
        };

        /// <summary>
        /// 活用形区分とその名称のテーブル　
        /// </summary>
        static public CFormIDName[] CFormIDNameArray = new CFormIDName[]
        {
            new CFormIDName { CFormID = "xx", CFormName = "*" },
            new CFormIDName { CFormID = "6", CFormName = "その他" },
            new CFormIDName { CFormID = "4", CFormName = "仮定形" },
            new CFormIDName { CFormID = "2", CFormName = "基本形" },
            new CFormIDName { CFormID = "0", CFormName = "未然形" },
            new CFormIDName { CFormID = "5", CFormName = "命令形" },
            new CFormIDName { CFormID = "3", CFormName = "連体形" },
            new CFormIDName { CFormID = "1", CFormName = "連用形" }
        };

        /// <summary>
        /// 活用形区分の名称を求める
        /// </summary>
        static public string GetCFormName(string CFormIDValue)
        {
            for (int i = 0; i < CFormIDNameArray.Length; i++)
            {
                if (CFormIDValue == CFormIDNameArray[i].CFormID)
                {
                    return CFormIDNameArray[i].CFormName;
                }
            };
            //見つからなければ空文字列を返す
            return "";
        }


        /// <summary>
        /// 活用種類区分とその名称の構造体
        /// </summary>
        public struct CTypeIDName
        {
            public string CTypeID;
            public string CTypeName;
        };

        /// <summary>
        /// 活用種類区分とその名称のテーブル
        /// </summary>
        static public CTypeIDName[] CTypeIDNameArray = new CTypeIDName[]
        {
            new CTypeIDName { CTypeID = "xx", CTypeName = "*" },
            new CTypeIDName { CTypeID = "5", CTypeName = "カ行変格" },
            new CTypeIDName { CTypeID = "4", CTypeName = "サ行変格" },
            new CTypeIDName { CTypeID = "6", CTypeName = "ラ行変格・四段・二段・不変化・文語助動詞" },
            new CTypeIDName { CTypeID = "3", CTypeName = "一段" },
            new CTypeIDName { CTypeID = "7", CTypeName = "形容詞" },
            new CTypeIDName { CTypeID = "1", CTypeName = "五段" },
            new CTypeIDName { CTypeID = "7", CTypeName = "助動詞" },
        };

        /// <summary>
        /// 活用種類区分の名称を求める
        /// </summary>
        static public string GetCTypeName(string CTypeIDValue)
        {
            for (int i = 0; i < CTypeIDNameArray.Length; i++)
            {
                if (CTypeIDValue == CTypeIDNameArray[i].CTypeID)
                {
                    return CTypeIDNameArray[i].CTypeName;
                }
            };
            //見つからなければ空文字列を返す
            return "";
        }

        /// <summary>
        /// トレース情報のコンストラクタ
        /// </summary>
        public TIF(string Line)
        {
            string[] block = Line.Split('/', ' ');
            this.PhonemeFrom = Int32.Parse(block[0]);
            this.PhonemeTo   = Int32.Parse(block[1]);
            string[] item = block[2].Split('^', '-', '+', '=');
            this.PhonemeBeforePrev = item[0];
            this.PhonemePrev       = item[1];
            this.PhonemeCurrent    = item[2];
            this.PhonemeNext       = item[3];
            this.PhonemeAfterNext  = item[4];
            item = block[3].Split(':', '+', '=');
            this.MoraDiffAccent  = item[1];
            this.MoraPosForward  = item[2];
            this.MoraPosBackward = item[3];
            this.MoraCurrent     = item[4];   //もともとのトレース情報にはないモーラ文字を取得している
            item = block[4].Split(':', '-', '_');
            this.POSIDPrev     = item[1];
            this.POSNamePrev   = GetPOSName(POSIDPrev);
            this.CFormIDPrev   = item[2];
            this.CFormNamePrev = GetCFormName(CFormIDPrev);
            this.CTypeIDPrev   = item[3];
            this.CTypeNamePrev = GetCTypeName(CTypeIDPrev);
            item = block[5].Split(':', '_', '+');
            this.POSIDCurrent     = item[1];
            this.POSNameCurrent   = GetPOSName(POSIDCurrent);
            this.CFormIDCurrent   = item[2];
            this.CFormNameCurrent = GetCFormName(CFormIDCurrent);
            this.CTypeIDCurrent   = item[3];
            this.CTypeNameCurrent = GetCTypeName(CTypeIDCurrent);
            item = block[6].Split(':', '+', '_');
            this.POSIDNext     = item[1];
            this.POSNameNext   = GetPOSName(POSIDNext);
            this.CFormIDNext   = item[2];
            this.CFormNameNext = GetCFormName(CFormIDNext);
            this.CTypeIDNext   = item[3];
            this.CTypeNameNext = GetCTypeName(CTypeIDNext);
            item = block[7].Split(':', '_', '!', '-');
            this.MoraNumPrevAcc         = item[1];
            this.AccTypePrevAcc         = item[2];
            this.IsInterrogativePrevAcc = item[3];
            this.UndefinedPrevAcc       = item[4];
            this.IsExistPausePrevAcc    = item[5];
            item = block[8].Split(':', '_', '#', '@', '|');
            this.MoraNumCurrentAcc               = item[1];
            this.AccTypeCurrentAcc               = item[2];
            this.IsInterrogativeCurrentAcc       = item[3];
            this.UndefinedCurrentAcc             = item[4];
            this.AccPosInBreathForward           = item[5];
            this.AccPosInBreathBackward          = item[6];
            this.FirstAccMoraPosInBreathForward  = item[7];
            this.FirstAccMoraPosInBreathBackward = item[8];
            item = block[9].Split(':', '_', '%');
            this.MoraNumNextAcc         = item[1];
            this.AccTypeNextAcc         = item[2];
            this.IsInterrogativeNextAcc = item[3];
            this.UndefinedNextAcc       = item[4];
            this.IsExistPauseNextAcc    = item[5];
            item = block[10].Split(':', '_');
            this.AccNumPrevBreath  = item[1];
            this.MoraNumPrevBreath = item[2];
            item = block[11].Split(':', '-', '@', '+', '&', '|');
            this.AccNumCurrentBreath                   = item[1];
            this.MoraNumCurrentBreath                  = item[2];
            this.BreathPosInUtteranceForward           = item[3];
            this.BreathPosInUtteranceBackward          = item[4];
            this.FirstAccPosInUtteranceForward         = item[5];
            this.FirstAccPosInUtteranceBackward        = item[6];
            this.FirstBreathMoraPosInUtteranceForward  = item[7];
            this.FirstBreathMoraPosInUtteranceBackward = item[8];
            item = block[12].Split(':', '_');
            this.AccNumNextBreath  = item[1];
            this.MoraNumNextBreath = item[2];
            item = block[13].Split(':', '+', '-');
            this.BreathNumInUtterance = item[1];
            this.AccNumInUtterance    = item[2];
            this.MoraNumInUtterance   = item[3];
        }

        /// <summary>
        /// シリアライズできるようにパラメータを持たないコンストラクタを用意する
        /// </summary>
        public TIF() { }

    }

}
