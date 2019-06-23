/*
 * Program.cs
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
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace TI2VSQX
{

    class Program
    {
        /// <summary>
        /// モーラ情報と音素をもとに、VOCALOIDの発音記号(X-SAMPA)を求めるためのテーブルのレコードの構造体
        /// </summary>
        public struct jpcommonMora
        {
            public string moraValue;
            public string LyricVSQX;
            public string PhonemeHead;
            public string PhonemeTail;
            public string PhonemeXSHead;
            public string PhonemeXSTail;
            public string PhonemeXSN;
        };

        /// <summary>
        /// モーラ情報と音素をもとに、VOCALOIDの発音記号(X-SAMPA)を求めるためのテーブル
        /// </summary>
        static public jpcommonMora[] jpcommonMoraArray = new jpcommonMora[]
        {
            new jpcommonMora { moraValue = "ヴョ", LyricVSQX = "ヴョ", PhonemeHead = "by", PhonemeTail = "o", PhonemeXSHead = "b"   , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴュ", LyricVSQX = "ヴュ", PhonemeHead = "by", PhonemeTail = "u", PhonemeXSHead = "b"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴャ", LyricVSQX = "ヴャ", PhonemeHead = "by", PhonemeTail = "a", PhonemeXSHead = "b"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴォ", LyricVSQX = "ヴォ", PhonemeHead = "v" , PhonemeTail = "o", PhonemeXSHead = "b"   , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴェ", LyricVSQX = "ヴェ", PhonemeHead = "v" , PhonemeTail = "e", PhonemeXSHead = "b"   , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴィ", LyricVSQX = "ヴィ", PhonemeHead = "v" , PhonemeTail = "i", PhonemeXSHead = "b"   , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴァ", LyricVSQX = "ヴァ", PhonemeHead = "v" , PhonemeTail = "a", PhonemeXSHead = "b"   , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴ"  , LyricVSQX = "ヴ",   PhonemeHead = "v" , PhonemeTail = "u", PhonemeXSHead = "b"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ン"  , LyricVSQX = "ん",   PhonemeHead = "N" , PhonemeTail = "" , PhonemeXSHead = "N\\" , PhonemeXSTail = ""  , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヲ"  , LyricVSQX = "を",   PhonemeHead = "o" , PhonemeTail = "" , PhonemeXSHead = "o"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヱ"  , LyricVSQX = "ゑ",   PhonemeHead = "e" , PhonemeTail = "" , PhonemeXSHead = "w e" , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヰ"  , LyricVSQX = "ゐ",   PhonemeHead = "i" , PhonemeTail = "" , PhonemeXSHead = "w i" , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ワ"  , LyricVSQX = "わ",   PhonemeHead = "w" , PhonemeTail = "a", PhonemeXSHead = "w"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヮ"  , LyricVSQX = "ゎ",   PhonemeHead = "w" , PhonemeTail = "a", PhonemeXSHead = "w"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ロ"  , LyricVSQX = "ろ",   PhonemeHead = "r" , PhonemeTail = "o", PhonemeXSHead = "4"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "レ"  , LyricVSQX = "れ",   PhonemeHead = "r" , PhonemeTail = "e", PhonemeXSHead = "4"   , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ル"  , LyricVSQX = "る",   PhonemeHead = "r" , PhonemeTail = "u", PhonemeXSHead = "4"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "リョ", LyricVSQX = "りょ", PhonemeHead = "ry", PhonemeTail = "o", PhonemeXSHead = "4'"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "リュ", LyricVSQX = "りゅ", PhonemeHead = "ry", PhonemeTail = "u", PhonemeXSHead = "4'"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "リャ", LyricVSQX = "りゃ", PhonemeHead = "ry", PhonemeTail = "a", PhonemeXSHead = "4'"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "リェ", LyricVSQX = "りぇ", PhonemeHead = "ry", PhonemeTail = "e", PhonemeXSHead = "4'"  , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "リ"  , LyricVSQX = "り",   PhonemeHead = "r" , PhonemeTail = "i", PhonemeXSHead = "4'"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ラ"  , LyricVSQX = "ら",   PhonemeHead = "r" , PhonemeTail = "a", PhonemeXSHead = "4"   , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヨ"  , LyricVSQX = "よ",   PhonemeHead = "y" , PhonemeTail = "o", PhonemeXSHead = "j"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ョ"  , LyricVSQX = "ょ",   PhonemeHead = "y" , PhonemeTail = "o", PhonemeXSHead = "j"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ユ"  , LyricVSQX = "ゆ",   PhonemeHead = "y" , PhonemeTail = "u", PhonemeXSHead = "j"   , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ュ"  , LyricVSQX = "ゅ",   PhonemeHead = "y" , PhonemeTail = "u", PhonemeXSHead = "j"   , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヤ"  , LyricVSQX = "や",   PhonemeHead = "y" , PhonemeTail = "a", PhonemeXSHead = "j"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ャ"  , LyricVSQX = "ゃ",   PhonemeHead = "y" , PhonemeTail = "a", PhonemeXSHead = "j"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "モ"  , LyricVSQX = "も",   PhonemeHead = "m" , PhonemeTail = "o", PhonemeXSHead = "m"   , PhonemeXSTail = "o" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "メ"  , LyricVSQX = "め",   PhonemeHead = "m" , PhonemeTail = "e", PhonemeXSHead = "m"   , PhonemeXSTail = "e" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ム"  , LyricVSQX = "む",   PhonemeHead = "m" , PhonemeTail = "u", PhonemeXSHead = "m"   , PhonemeXSTail = "M" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ミョ", LyricVSQX = "みょ", PhonemeHead = "my", PhonemeTail = "o", PhonemeXSHead = "m'"  , PhonemeXSTail = "o" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ミュ", LyricVSQX = "みゅ", PhonemeHead = "my", PhonemeTail = "u", PhonemeXSHead = "m'"  , PhonemeXSTail = "M" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ミェ", LyricVSQX = "みぇ", PhonemeHead = "my", PhonemeTail = "e", PhonemeXSHead = "m'"  , PhonemeXSTail = "e" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ミャ", LyricVSQX = "みゃ", PhonemeHead = "my", PhonemeTail = "a", PhonemeXSHead = "m'"  , PhonemeXSTail = "a" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ミ"  , LyricVSQX = "み",   PhonemeHead = "m" , PhonemeTail = "i", PhonemeXSHead = "m'"  , PhonemeXSTail = "i" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "マ"  , LyricVSQX = "ま",   PhonemeHead = "m" , PhonemeTail = "a", PhonemeXSHead = "m"   , PhonemeXSTail = "a" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ポ"  , LyricVSQX = "ぽ",   PhonemeHead = "p" , PhonemeTail = "o", PhonemeXSHead = "p"   , PhonemeXSTail = "o" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ボ"  , LyricVSQX = "ぼ",   PhonemeHead = "b" , PhonemeTail = "o", PhonemeXSHead = "b"   , PhonemeXSTail = "o" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ホ"  , LyricVSQX = "ほ",   PhonemeHead = "h" , PhonemeTail = "o", PhonemeXSHead = "h"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ペ"  , LyricVSQX = "ぺ",   PhonemeHead = "p" , PhonemeTail = "e", PhonemeXSHead = "p"   , PhonemeXSTail = "e" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ベ"  , LyricVSQX = "べ",   PhonemeHead = "b" , PhonemeTail = "e", PhonemeXSHead = "b"   , PhonemeXSTail = "e" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ヘ"  , LyricVSQX = "へ",   PhonemeHead = "h" , PhonemeTail = "e", PhonemeXSHead = "h"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "プ"  , LyricVSQX = "ぷ",   PhonemeHead = "p" , PhonemeTail = "u", PhonemeXSHead = "p"   , PhonemeXSTail = "M" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ブ"  , LyricVSQX = "ぶ",   PhonemeHead = "b" , PhonemeTail = "u", PhonemeXSHead = "b"   , PhonemeXSTail = "M" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "フォ", LyricVSQX = "ふぉ", PhonemeHead = "f" , PhonemeTail = "o", PhonemeXSHead = "p\\" , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "フェ", LyricVSQX = "ふぇ", PhonemeHead = "f" , PhonemeTail = "e", PhonemeXSHead = "p\\" , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "フィ", LyricVSQX = "ふぃ", PhonemeHead = "f" , PhonemeTail = "i", PhonemeXSHead = "p\\'", PhonemeXSTail = "i" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ファ", LyricVSQX = "ふぁ", PhonemeHead = "f" , PhonemeTail = "a", PhonemeXSHead = "p\\" , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "フ"  , LyricVSQX = "ふ",   PhonemeHead = "f" , PhonemeTail = "u", PhonemeXSHead = "p'"  , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ピョ", LyricVSQX = "ぴょ", PhonemeHead = "py", PhonemeTail = "o", PhonemeXSHead = "p'"  , PhonemeXSTail = "o" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ピュ", LyricVSQX = "ぴゅ", PhonemeHead = "py", PhonemeTail = "u", PhonemeXSHead = "p'"  , PhonemeXSTail = "M" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ピャ", LyricVSQX = "ぴゃ", PhonemeHead = "py", PhonemeTail = "a", PhonemeXSHead = "p'"  , PhonemeXSTail = "a" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ピェ", LyricVSQX = "ぴぇ", PhonemeHead = "py", PhonemeTail = "e", PhonemeXSHead = "p'"  , PhonemeXSTail = "e" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ピ"  , LyricVSQX = "ぴ",   PhonemeHead = "p" , PhonemeTail = "i", PhonemeXSHead = "p'"  , PhonemeXSTail = "i" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ビョ", LyricVSQX = "びょ", PhonemeHead = "by", PhonemeTail = "o", PhonemeXSHead = "b'"  , PhonemeXSTail = "o" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ビュ", LyricVSQX = "びゅ", PhonemeHead = "by", PhonemeTail = "u", PhonemeXSHead = "b'"  , PhonemeXSTail = "M" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ビャ", LyricVSQX = "びゃ", PhonemeHead = "by", PhonemeTail = "a", PhonemeXSHead = "b'"  , PhonemeXSTail = "a" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ビェ", LyricVSQX = "びぇ", PhonemeHead = "by", PhonemeTail = "e", PhonemeXSHead = "b'"  , PhonemeXSTail = "e" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ビ"  , LyricVSQX = "び",   PhonemeHead = "b" , PhonemeTail = "i", PhonemeXSHead = "b'"  , PhonemeXSTail = "i" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ヒョ", LyricVSQX = "ひょ", PhonemeHead = "hy", PhonemeTail = "o", PhonemeXSHead = "C"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヒュ", LyricVSQX = "ひゅ", PhonemeHead = "hy", PhonemeTail = "u", PhonemeXSHead = "C"   , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヒャ", LyricVSQX = "ひゃ", PhonemeHead = "hy", PhonemeTail = "a", PhonemeXSHead = "C"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヒェ", LyricVSQX = "ひぇ", PhonemeHead = "hy", PhonemeTail = "e", PhonemeXSHead = "C"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヒ"  , LyricVSQX = "ひ",   PhonemeHead = "h" , PhonemeTail = "i", PhonemeXSHead = "C"   , PhonemeXSTail = "i" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "パ"  , LyricVSQX = "ぱ",   PhonemeHead = "p" , PhonemeTail = "a", PhonemeXSHead = "p"   , PhonemeXSTail = "a" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "バ"  , LyricVSQX = "ぱ",   PhonemeHead = "b" , PhonemeTail = "a", PhonemeXSHead = "b"   , PhonemeXSTail = "a" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ハ"  , LyricVSQX = "は",   PhonemeHead = "h" , PhonemeTail = "a", PhonemeXSHead = "h"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ノ"  , LyricVSQX = "の",   PhonemeHead = "n" , PhonemeTail = "o", PhonemeXSHead = "n"   , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ネ"  , LyricVSQX = "ね",   PhonemeHead = "n" , PhonemeTail = "e", PhonemeXSHead = "n"   , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヌ"  , LyricVSQX = "ぬ",   PhonemeHead = "n" , PhonemeTail = "u", PhonemeXSHead = "n"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ニョ", LyricVSQX = "にょ", PhonemeHead = "ny", PhonemeTail = "o", PhonemeXSHead = "J"   , PhonemeXSTail = "o" , PhonemeXSN = "J" },
            new jpcommonMora { moraValue = "ニュ", LyricVSQX = "にゅ", PhonemeHead = "ny", PhonemeTail = "u", PhonemeXSHead = "J"   , PhonemeXSTail = "M" , PhonemeXSN = "J" },
            new jpcommonMora { moraValue = "ニャ", LyricVSQX = "にゃ", PhonemeHead = "ny", PhonemeTail = "a", PhonemeXSHead = "J"   , PhonemeXSTail = "a" , PhonemeXSN = "J" },
            new jpcommonMora { moraValue = "ニェ", LyricVSQX = "にぇ", PhonemeHead = "ny", PhonemeTail = "e", PhonemeXSHead = "J"   , PhonemeXSTail = "e" , PhonemeXSN = "J" },
            new jpcommonMora { moraValue = "ニ"  , LyricVSQX = "に",   PhonemeHead = "n" , PhonemeTail = "i", PhonemeXSHead = "J"   , PhonemeXSTail = "i" , PhonemeXSN = "J" },
            new jpcommonMora { moraValue = "ナ"  , LyricVSQX = "な",   PhonemeHead = "n" , PhonemeTail = "a", PhonemeXSHead = "n"   , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ドゥ", LyricVSQX = "どぅ", PhonemeHead = "d" , PhonemeTail = "u", PhonemeXSHead = "d"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ド"  , LyricVSQX = "ど",   PhonemeHead = "d" , PhonemeTail = "o", PhonemeXSHead = "d"   , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "トゥ", LyricVSQX = "とぅ", PhonemeHead = "t" , PhonemeTail = "u", PhonemeXSHead = "t"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ト"  , LyricVSQX = "と",   PhonemeHead = "t" , PhonemeTail = "o", PhonemeXSHead = "t"   , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "デョ", LyricVSQX = "でょ", PhonemeHead = "dy", PhonemeTail = "o", PhonemeXSHead = "d'"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "デュ", LyricVSQX = "でゅ", PhonemeHead = "dy", PhonemeTail = "u", PhonemeXSHead = "d'"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "デャ", LyricVSQX = "でゃ", PhonemeHead = "dy", PhonemeTail = "a", PhonemeXSHead = "d'"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ディ", LyricVSQX = "でぃ", PhonemeHead = "d" , PhonemeTail = "i", PhonemeXSHead = "d'"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "デ"  , LyricVSQX = "で",   PhonemeHead = "d" , PhonemeTail = "e", PhonemeXSHead = "d"   , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "テョ", LyricVSQX = "てょ", PhonemeHead = "ty", PhonemeTail = "o", PhonemeXSHead = "t'"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "テュ", LyricVSQX = "てゅ", PhonemeHead = "ty", PhonemeTail = "u", PhonemeXSHead = "t'"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "テャ", LyricVSQX = "てゃ", PhonemeHead = "ty", PhonemeTail = "a", PhonemeXSHead = "t'"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ティ", LyricVSQX = "てぃ", PhonemeHead = "t" , PhonemeTail = "i", PhonemeXSHead = "t'"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "テ"  , LyricVSQX = "て",   PhonemeHead = "t" , PhonemeTail = "e", PhonemeXSHead = "t"   , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヅ"  , LyricVSQX = "づ",   PhonemeHead = "z" , PhonemeTail = "u", PhonemeXSHead = "dz"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ツォ", LyricVSQX = "つぉ", PhonemeHead = "ts", PhonemeTail = "o", PhonemeXSHead = "ts"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ツェ", LyricVSQX = "つぇ", PhonemeHead = "ts", PhonemeTail = "e", PhonemeXSHead = "ts"  , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ツィ", LyricVSQX = "つぃ", PhonemeHead = "ts", PhonemeTail = "i", PhonemeXSHead = "ts"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ツァ", LyricVSQX = "つぁ", PhonemeHead = "ts", PhonemeTail = "a", PhonemeXSHead = "ts"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ツ"  , LyricVSQX = "つ",   PhonemeHead = "ts", PhonemeTail = "u", PhonemeXSHead = "ts"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ッ"  , LyricVSQX = "っ",   PhonemeHead = "cl", PhonemeTail = "" , PhonemeXSHead = "sil" , PhonemeXSTail = ""  , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヂ"  , LyricVSQX = "ぢ",   PhonemeHead = "j" , PhonemeTail = "i", PhonemeXSHead = "dZ"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "チョ", LyricVSQX = "ちょ", PhonemeHead = "ch", PhonemeTail = "o", PhonemeXSHead = "tS"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "チュ", LyricVSQX = "ちゅ", PhonemeHead = "ch", PhonemeTail = "u", PhonemeXSHead = "tS"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "チャ", LyricVSQX = "ちゃ", PhonemeHead = "ch", PhonemeTail = "a", PhonemeXSHead = "tS"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "チェ", LyricVSQX = "ちぇ", PhonemeHead = "ch", PhonemeTail = "e", PhonemeXSHead = "tS"  , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "チ"  , LyricVSQX = "ち",   PhonemeHead = "ch", PhonemeTail = "i", PhonemeXSHead = "tS"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ダ"  , LyricVSQX = "だ",   PhonemeHead = "d" , PhonemeTail = "a", PhonemeXSHead = "d"   , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "タ"  , LyricVSQX = "た",   PhonemeHead = "t" , PhonemeTail = "a", PhonemeXSHead = "t"   , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ゾ"  , LyricVSQX = "ぞ",   PhonemeHead = "z" , PhonemeTail = "o", PhonemeXSHead = "dz"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ソ"  , LyricVSQX = "そ",   PhonemeHead = "s" , PhonemeTail = "o", PhonemeXSHead = "s"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ゼ"  , LyricVSQX = "ぜ",   PhonemeHead = "z" , PhonemeTail = "e", PhonemeXSHead = "dz"  , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "セ"  , LyricVSQX = "せ",   PhonemeHead = "s" , PhonemeTail = "e", PhonemeXSHead = "s"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ズィ", LyricVSQX = "ずぃ", PhonemeHead = "z" , PhonemeTail = "i", PhonemeXSHead = "dz"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ズ"  , LyricVSQX = "ず",   PhonemeHead = "z" , PhonemeTail = "u", PhonemeXSHead = "dz"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "スィ", LyricVSQX = "すぃ", PhonemeHead = "s" , PhonemeTail = "i", PhonemeXSHead = "s"   , PhonemeXSTail = "i" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ス"  , LyricVSQX = "す",   PhonemeHead = "s" , PhonemeTail = "u", PhonemeXSHead = "s"   , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ジョ", LyricVSQX = "じょ", PhonemeHead = "j" , PhonemeTail = "o", PhonemeXSHead = "dZ"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ジュ", LyricVSQX = "じゅ", PhonemeHead = "j" , PhonemeTail = "u", PhonemeXSHead = "dZ"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ジャ", LyricVSQX = "じゃ", PhonemeHead = "j" , PhonemeTail = "a", PhonemeXSHead = "dZ"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ジェ", LyricVSQX = "じぇ", PhonemeHead = "j" , PhonemeTail = "e", PhonemeXSHead = "dZ"  , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ジ"  , LyricVSQX = "じ",   PhonemeHead = "j" , PhonemeTail = "i", PhonemeXSHead = "dZ"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ショ", LyricVSQX = "しょ", PhonemeHead = "sh", PhonemeTail = "o", PhonemeXSHead = "S"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "シュ", LyricVSQX = "しゅ", PhonemeHead = "sh", PhonemeTail = "u", PhonemeXSHead = "S"   , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "シャ", LyricVSQX = "しゃ", PhonemeHead = "sh", PhonemeTail = "a", PhonemeXSHead = "S"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "シェ", LyricVSQX = "しぇ", PhonemeHead = "sh", PhonemeTail = "e", PhonemeXSHead = "S"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "シ"  , LyricVSQX = "し",   PhonemeHead = "sh", PhonemeTail = "i", PhonemeXSHead = "S"   , PhonemeXSTail = "i" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ザ"  , LyricVSQX = "ざ",   PhonemeHead = "z" , PhonemeTail = "a", PhonemeXSHead = "dz"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "サ"  , LyricVSQX = "さ",   PhonemeHead = "s" , PhonemeTail = "a", PhonemeXSHead = "s"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ゴ"  , LyricVSQX = "ご",   PhonemeHead = "g" , PhonemeTail = "o", PhonemeXSHead = "g"   , PhonemeXSTail = "o" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "コ"  , LyricVSQX = "こ",   PhonemeHead = "k" , PhonemeTail = "o", PhonemeXSHead = "k"   , PhonemeXSTail = "o" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "ゲ"  , LyricVSQX = "げ",   PhonemeHead = "g" , PhonemeTail = "e", PhonemeXSHead = "g"   , PhonemeXSTail = "e" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "ケ"  , LyricVSQX = "け",   PhonemeHead = "k" , PhonemeTail = "e", PhonemeXSHead = "k"   , PhonemeXSTail = "e" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "ヶ"  , LyricVSQX = "ヶ",   PhonemeHead = "k" , PhonemeTail = "e", PhonemeXSHead = "k"   , PhonemeXSTail = "e" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "グヮ", LyricVSQX = "グヮ", PhonemeHead = "gw", PhonemeTail = "a", PhonemeXSHead = "g w" , PhonemeXSTail = "a" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "グ"  , LyricVSQX = "ぐ",   PhonemeHead = "g" , PhonemeTail = "u", PhonemeXSHead = "g"   , PhonemeXSTail = "M" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "クヮ", LyricVSQX = "クヮ", PhonemeHead = "kw", PhonemeTail = "a", PhonemeXSHead = "k w" , PhonemeXSTail = "a" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "ク"  , LyricVSQX = "く",   PhonemeHead = "k" , PhonemeTail = "u", PhonemeXSHead = "k"   , PhonemeXSTail = "M" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "ギョ", LyricVSQX = "ぎょ", PhonemeHead = "gy", PhonemeTail = "o", PhonemeXSHead = "g'"  , PhonemeXSTail = "o" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "ギュ", LyricVSQX = "ぎゅ", PhonemeHead = "gy", PhonemeTail = "u", PhonemeXSHead = "g'"  , PhonemeXSTail = "M" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "ギャ", LyricVSQX = "ぎゃ", PhonemeHead = "gy", PhonemeTail = "a", PhonemeXSHead = "g'"  , PhonemeXSTail = "a" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "ギェ", LyricVSQX = "ぎぇ", PhonemeHead = "gy", PhonemeTail = "e", PhonemeXSHead = "g'"  , PhonemeXSTail = "e" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "ギ"  , LyricVSQX = "ぎ",   PhonemeHead = "g" , PhonemeTail = "i", PhonemeXSHead = "g'"  , PhonemeXSTail = "i" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "キョ", LyricVSQX = "きょ", PhonemeHead = "ky", PhonemeTail = "o", PhonemeXSHead = "k'"  , PhonemeXSTail = "o" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "キュ", LyricVSQX = "きゅ", PhonemeHead = "ky", PhonemeTail = "u", PhonemeXSHead = "k'"  , PhonemeXSTail = "M" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "キャ", LyricVSQX = "きゃ", PhonemeHead = "ky", PhonemeTail = "a", PhonemeXSHead = "k'"  , PhonemeXSTail = "a" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "キェ", LyricVSQX = "きぇ", PhonemeHead = "ky", PhonemeTail = "e", PhonemeXSHead = "k'"  , PhonemeXSTail = "e" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "キ"  , LyricVSQX = "き",   PhonemeHead = "k" , PhonemeTail = "i", PhonemeXSHead = "k'"  , PhonemeXSTail = "i" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "ガ"  , LyricVSQX = "が",   PhonemeHead = "g" , PhonemeTail = "a", PhonemeXSHead = "g"   , PhonemeXSTail = "a" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "カ"  , LyricVSQX = "か",   PhonemeHead = "k" , PhonemeTail = "a", PhonemeXSHead = "k"   , PhonemeXSTail = "a" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "オ"  , LyricVSQX = "お",   PhonemeHead = "o" , PhonemeTail = "" , PhonemeXSHead = "o"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ォ"  , LyricVSQX = "ぉ",   PhonemeHead = "o" , PhonemeTail = "" , PhonemeXSHead = "o"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "エ"  , LyricVSQX = "え",   PhonemeHead = "e" , PhonemeTail = "" , PhonemeXSHead = "e"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ェ"  , LyricVSQX = "ぇ",   PhonemeHead = "e" , PhonemeTail = "" , PhonemeXSHead = "e"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ウォ", LyricVSQX = "うぉ", PhonemeHead = "w" , PhonemeTail = "o", PhonemeXSHead = "w"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ウェ", LyricVSQX = "うぇ", PhonemeHead = "w" , PhonemeTail = "e", PhonemeXSHead = "w"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ウィ", LyricVSQX = "うぃ", PhonemeHead = "w" , PhonemeTail = "i", PhonemeXSHead = "w"   , PhonemeXSTail = "i" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ウ"  , LyricVSQX = "う",   PhonemeHead = "u" , PhonemeTail = "" , PhonemeXSHead = "M"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ゥ"  , LyricVSQX = "ぅ",   PhonemeHead = "u" , PhonemeTail = "" , PhonemeXSHead = "M"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "イェ", LyricVSQX = "いぇ", PhonemeHead = "y" , PhonemeTail = "e", PhonemeXSHead = "j"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "イ"  , LyricVSQX = "い",   PhonemeHead = "i" , PhonemeTail = "" , PhonemeXSHead = "i"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ィ"  , LyricVSQX = "ぃ",   PhonemeHead = "i" , PhonemeTail = "" , PhonemeXSHead = "i"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ア"  , LyricVSQX = "あ",   PhonemeHead = "a" , PhonemeTail = "" , PhonemeXSHead = "a"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ァ"  , LyricVSQX = "ぁ",   PhonemeHead = "a" , PhonemeTail = "" , PhonemeXSHead = "a"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ー"  , LyricVSQX = "ー",   PhonemeHead = "a" , PhonemeTail = "" , PhonemeXSHead = "-"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ー"  , LyricVSQX = "ー",   PhonemeHead = "i" , PhonemeTail = "" , PhonemeXSHead = "-"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ー"  , LyricVSQX = "ー",   PhonemeHead = "u" , PhonemeTail = "" , PhonemeXSHead = "-"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ー"  , LyricVSQX = "ー",   PhonemeHead = "e" , PhonemeTail = "" , PhonemeXSHead = "-"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ー"  , LyricVSQX = "ー",   PhonemeHead = "o" , PhonemeTail = "" , PhonemeXSHead = "-"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = ""    , LyricVSQX = "",     PhonemeHead = ""  , PhonemeTail = "" , PhonemeXSHead = ""    , PhonemeXSTail = ""  , PhonemeXSN = "" }
        };                                                     
                                                               
        /// <summary>                                          
        /// モーラの文字と音素から発音記号（X-SAMPA）を求める  
        /// </summary>                                         
        /// <param name="moraValue">モーラの文字</param>       
        /// <param name="Phoneme">現在の音素</param>           
        /// <param name="nextPhonemeValue">次の音素</param>    
        /// <returns>発音記号</returns>                        
        static public string PhonemeXS(string moraValue, string Phoneme,string nextPhonemeValue)
        {

            //音素の無声化情報を削除する。無声化情報は変数に格納する。
            bool IsVoiceless = false;
            switch (Phoneme)                                   
            {                                                  
                case "A":                                      
                    Phoneme = "a";
                    IsVoiceless = true;
                    break;                                     
                case "I":                                      
                    Phoneme = "i";
                    IsVoiceless = true;
                    break;                                     
                case "U":                                      
                    Phoneme = "u";
                    IsVoiceless = true;
                    break;                                     
                case "E":                                      
                    Phoneme = "e";
                    IsVoiceless = true;
                    break;                                     
                case "O":                                      
                    Phoneme = "o";
                    IsVoiceless = true;
                    break;                                     
            };                                                 
                                                               
            //無声化してモーラに「’」がある場合は取り除いて判定に使う
            if (moraValue.Length > 1)                          
            {                                                  
                if (moraValue.Substring(1, 1) == "’")         
                {                                              
                    moraValue = moraValue.Substring(0, 1);     
                }                                              
            }                                                  
                                                               
            //発音記号はモーラと音素をもとに配列検索して求める 
            for (int i = 0; i < jpcommonMoraArray.Length; i++ )
            {                                                  
                if ((moraValue == jpcommonMoraArray[i].moraValue) &
                    (Phoneme   == jpcommonMoraArray[i].PhonemeHead))
                {                                              
                    return jpcommonMoraArray[i].PhonemeXSHead ;
                }else                                          
                if ((moraValue == jpcommonMoraArray[i].moraValue) &
                    (Phoneme   == jpcommonMoraArray[i].PhonemeTail))
                {
                    //文末で「う」で無声化している場合は母音部分に「Asp」を設定する
                    if ((Phoneme == "u") &
                        (IsVoiceless == true) &
                        (nextPhonemeValue == "sil" | nextPhonemeValue == "pau"))
                    {
                        return jpcommonMoraArray[i].PhonemeXSHead + " " + "Asp";
                    }
                    else
                    {
                        return jpcommonMoraArray[i].PhonemeXSHead + " " + jpcommonMoraArray[i].PhonemeXSTail;
                    }
                }                                              
            };                                                 
            return Phoneme;                                    
        }                                                      
                                                               
        /// <summary>                                          
        /// 音素から「ん」の発音記号（X-SAMPA）を求める（Wikipedia「日本語の音韻」の「撥音/N/の子音」の項目参照）
        /// </summary>                                         
        /// <param name="nextPhonemeValue">次の音素</param>    
        /// <param name="afternextPhonemeValue">次の次の音素</param>
        /// <returns>発音記号</returns>                        
        static public string PhonemeXSN(string nextPhonemeValue, string afternextPhonemeValue)
        {                                                      
            //「ん」の発音記号は次とその次の音素をもとに配列検索して求める
            for (int i = 0; i < jpcommonMoraArray.Length; i++)
            {                                                  
                if ((nextPhonemeValue      == jpcommonMoraArray[i].PhonemeHead) &
                    (afternextPhonemeValue == jpcommonMoraArray[i].PhonemeTail))
                {                                              
                    return jpcommonMoraArray[i].PhonemeXSN;    
                }                                              
            };                                                 
            //「ん」の次の音素が文末で母音の場合向けに次の音素だけで検索
            for (int i = 0; i < jpcommonMoraArray.Length; i++)
            {                                                  
                if (nextPhonemeValue == jpcommonMoraArray[i].PhonemeHead) 
                {                                              
                    return jpcommonMoraArray[i].PhonemeXSN;    
                }                                              
            };                                                 
            //見つからなければ文末扱い                         
            return "N\\";                                      
        }

        /// <summary>                                          
        /// モーラの文字と音素から歌詞を求める  
        /// </summary>                                         
        /// <param name="moraValue">モーラの文字</param>       
        /// <param name="Phoneme">現在の音素</param>           
        /// <returns>歌詞</returns>                        
        static public string moraToLyric(string moraValue, string Phoneme)
        {

            //無声化情報を削除する
            switch (Phoneme)
            {
                case "A":
                    Phoneme = "a";
                    break;
                case "I":
                    Phoneme = "i";
                    break;
                case "U":
                    Phoneme = "u";
                    break;
                case "E":
                    Phoneme = "e";
                    break;
                case "O":
                    Phoneme = "o";
                    break;
            };

            //無声化してモーラに「’」がある場合は取り除いて判定に使う
            if (moraValue.Length > 1)
            {
                if (moraValue.Substring(1, 1) == "’")
                {
                    moraValue = moraValue.Substring(0, 1);
                }
            }

            //歌詞はモーラと音素をもとに配列検索して求める 
            for (int i = 0; i < jpcommonMoraArray.Length; i++)
            {
                if ((moraValue == jpcommonMoraArray[i].moraValue) &
                    (Phoneme == jpcommonMoraArray[i].PhonemeHead))
                {
                    //あ段の母音・「っ」「ん」で合致した場合は発音記号をそのまま変換する。
                    switch (Phoneme)
                    {
                        case "a":
                            return "あ";
                        case "i":
                            return "い";
                        case "u":
                            return "う";
                        case "e":
                            return "え";
                        case "o":
                            return "お";
                        case "cl":
                            return "っ";
                        case "N":
                            return "ん";
                        //子音で合致した場合は発音記号をそのまま編集する。
                        default:
                            return Phoneme;
                    }
                }
                else
                //母音部分にモーラ文字をひらがな変換した文字を編集する 
                if ((moraValue == jpcommonMoraArray[i].moraValue) &
                    (Phoneme == jpcommonMoraArray[i].PhonemeTail))
                {
                    return jpcommonMoraArray[i].LyricVSQX;
                }
            };
            return Phoneme;
        }

        /// <summary>                                          
        /// モーラの文字と音素から発音記号（X-SAMPA）を求める(母音音符モード用)  
        /// </summary>                                         
        /// <param name="moraValue">モーラの文字</param>       
        /// <param name="Phoneme">現在の音素</param>           
        /// <param name="nextPhonemeValue">次の音素</param>    
        /// <param name="prevPhonemeValue">前の音素</param>    
        /// <returns>発音記号</returns>                        
        static public string PhonemeXS2(string moraValue, string Phoneme, string nextPhonemeValue, string prevPhonemeValue)
        {

            //音素の無声化情報を削除する。無声化情報は変数に格納する。
            bool IsVoiceless = false;
            switch (Phoneme)
            {
                case "A":
                    Phoneme = "a";
                    IsVoiceless = true;
                    break;
                case "I":
                    Phoneme = "i";
                    IsVoiceless = true;
                    break;
                case "U":
                    Phoneme = "u";
                    IsVoiceless = true;
                    break;
                case "E":
                    Phoneme = "e";
                    IsVoiceless = true;
                    break;
                case "O":
                    Phoneme = "o";
                    IsVoiceless = true;
                    break;
            };

            //前の音素の無声化情報を削除する。
            switch (prevPhonemeValue)
            {
                case "A":
                    prevPhonemeValue = "a";
                    break;
                case "I":
                    prevPhonemeValue = "i";
                    break;
                case "U":
                    prevPhonemeValue = "u";
                    break;
                case "E":
                    prevPhonemeValue = "e";
                    break;
                case "O":
                    prevPhonemeValue = "o";
                    break;
            };

            //無声化してモーラに「’」がある場合は取り除いて判定に使う
            if (moraValue.Length > 1)
            {
                if (moraValue.Substring(1, 1) == "’")
                {
                    moraValue = moraValue.Substring(0, 1);
                }
            }

            //発音記号はモーラと音素をもとに配列検索して求める 
            for (int i = 0; i < jpcommonMoraArray.Length; i++)
            {
                if ((moraValue == jpcommonMoraArray[i].moraValue) &
                    (Phoneme == jpcommonMoraArray[i].PhonemeHead))
                {
                    //あ段の母音・「っ」「ん」で合致した場合は発音記号をそのまま変換する。
                    switch (Phoneme)
                    {
                        case "a":
                        case "i":
                        case "u":
                        case "e":
                        case "o":
                        case "cl":
                        case "N":
                            return jpcommonMoraArray[i].PhonemeXSHead;
                        default:
                            //子音で合致した場合は前の音素が母音なら引き継ぎ、それ以外は「Asp」にする。
                            switch (prevPhonemeValue)
                            {
                                case "a":
                                    Phoneme = "a";
                                    break;
                                case "i":
                                    Phoneme = "i";
                                    break;
                                case "u":
                                    Phoneme = "M";
                                    break;
                                case "e":
                                    Phoneme = "e";
                                    break;
                                case "o":
                                    Phoneme = "o";
                                    break;
                                //「ん」の場合は本来さらに手前の発音記号をもとに設定するのが正しいが簡易版として「n」を使用する。
                                //母音音符モードはVOCALOID以外の音声合成エンジンを意識した物であり発音記号はそれほど使わないため。
                                case "N":
                                    Phoneme = "n";
                                    break;
                                default:
                                    Phoneme = "Asp";
                                    break;
                            }
                            break;
                    }
                    return Phoneme;
                }
                else
                if ((moraValue == jpcommonMoraArray[i].moraValue) &
                    (Phoneme == jpcommonMoraArray[i].PhonemeTail))
                {
                    //文末で「う」で無声化している場合は母音部分に「Asp」を設定する
                    if ((Phoneme == "u") &
                        (IsVoiceless == true) &
                        (nextPhonemeValue == "sil" | nextPhonemeValue == "pau"))
                    {
                        return jpcommonMoraArray[i].PhonemeXSHead + " " + "Asp";
                    }
                    else
                    {
                        return jpcommonMoraArray[i].PhonemeXSHead + " " + jpcommonMoraArray[i].PhonemeXSTail;
                    }
                }
            };
            return Phoneme;
        }

        /// <summary>                                          
        /// モーラの文字と音素から歌詞を求める(母音音符モード用)  
        /// </summary>                                         
        /// <param name="moraValue">モーラの文字</param>       
        /// <param name="Phoneme">現在の音素</param>           
        /// <returns>歌詞</returns>                        
        static public string moraToLyric2(string moraValue, string Phoneme,string prevPhonemeValue)
        {

            //無声化情報を削除する
            switch (Phoneme)
            {
                case "A":
                    Phoneme = "a";
                    break;
                case "I":
                    Phoneme = "i";
                    break;
                case "U":
                    Phoneme = "u";
                    break;
                case "E":
                    Phoneme = "e";
                    break;
                case "O":
                    Phoneme = "o";
                    break;
            };

            //前の音素の無声化情報を削除する。
            switch (prevPhonemeValue)
            {
                case "A":
                    prevPhonemeValue = "a";
                    break;
                case "I":
                    prevPhonemeValue = "i";
                    break;
                case "U":
                    prevPhonemeValue = "u";
                    break;
                case "E":
                    prevPhonemeValue = "e";
                    break;
                case "O":
                    prevPhonemeValue = "o";
                    break;
            };

            //無声化してモーラに「’」がある場合は取り除いて判定に使う
            if (moraValue.Length > 1)
            {
                if (moraValue.Substring(1, 1) == "’")
                {
                    moraValue = moraValue.Substring(0, 1);
                }
            }

            //歌詞はモーラと音素をもとに配列検索して求める 
            for (int i = 0; i < jpcommonMoraArray.Length; i++)
            {
                if ((moraValue == jpcommonMoraArray[i].moraValue) &
                    (Phoneme == jpcommonMoraArray[i].PhonemeHead))
                {
                    //発音記号があ段の母音・「っ」「ん」で合致した場合は、その発音記号から変換する。
                    //モーラ文字が長音の場合もここで歌詞変換する。「っ」はUTAUの休符である「R」にする。
                    switch (Phoneme)
                    {
                        case "a":
                            return "あ";
                        case "i":
                            return "い";
                        case "u":
                            return "う";
                        case "e":
                            return "え";
                        case "o":
                            return "お";
                        case "cl":
                            return "R";
                        case "N":
                            return "ん";
                        //子音で合致した場合は前の音素が母音なら引き継ぎ、それ以外はUTAUの休符である「R」にする。
                        default:
                            switch (prevPhonemeValue)
                            {
                                case "a":
                                    return "あ";
                                case "i":
                                    return "い";
                                case "u":
                                    return "う";
                                case "e":
                                    return "え";
                                case "o":
                                    return "お";
                                case "cl":
                                    return "R";
                                case "N":
                                    return "ん";
                                default:
                                    return "R";
                            }
                    }
                }
                else
                //母音部分にモーラ文字をひらがな変換した文字を編集する 
                if ((moraValue == jpcommonMoraArray[i].moraValue) &
                    (Phoneme == jpcommonMoraArray[i].PhonemeTail))
                {
                    return jpcommonMoraArray[i].LyricVSQX;
                }
            };
            return Phoneme;
        }

        /// <summary>                                          
        /// vsq3オブジェクト(VOCALOID3用VSQX)を初期化する      
        /// </summary>                                         
        /// <param name="compIDValue">シンガーのcompID</param>
        /// <param name="vVoiceNamevalue">シンガー名</param>   
        /// <returns>vsq3クラスのオブジェクト（初期設定済）</returns>
        static vsq3 VSQX_Init(string compIDValue, string vVoiceNamevalue)
        {                                                      
            vsq3 VSQX = new vsq3();                            
                                                               
            VSQX.vender = new XmlDocument().CreateCDataSection("Yamaha corporation");
            VSQX.version = new XmlDocument().CreateCDataSection("3.0.0.11");

            VSQX.vVoiceTable = new vVoice[] { new vVoice { } };

            VSQX.vVoiceTable[0].compID = new XmlDocument().CreateCDataSection(compIDValue);
            VSQX.vVoiceTable[0].vBS = 0;
            VSQX.vVoiceTable[0].vPC = 0;
            VSQX.vVoiceTable[0].vVoiceName = new XmlDocument().CreateCDataSection(vVoiceNamevalue);

            VSQX.vVoiceTable[0].vVoiceParam = new vVoiceParam { };

            VSQX.vVoiceTable[0].vVoiceParam.bre = 0;
            VSQX.vVoiceTable[0].vVoiceParam.bri = 0;
            VSQX.vVoiceTable[0].vVoiceParam.cle = 0;
            VSQX.vVoiceTable[0].vVoiceParam.gen = 0;
            VSQX.vVoiceTable[0].vVoiceParam.ope = 0;

            VSQX.mixer = new mixer { };

            VSQX.mixer.masterUnit = new masterUnit { };

            VSQX.mixer.masterUnit.outDev   = 0;
            VSQX.mixer.masterUnit.retLevel = 0;
            VSQX.mixer.masterUnit.vol      = 0;

            VSQX.mixer.vsUnit = new vsUnit[] { new vsUnit { } };

            VSQX.mixer.vsUnit[0].vsTrackNo  = 0;
            VSQX.mixer.vsUnit[0].inGain     = 0;
            VSQX.mixer.vsUnit[0].sendLevel  = -898;
            VSQX.mixer.vsUnit[0].sendEnable = 0;
            VSQX.mixer.vsUnit[0].mute       = 0;
            VSQX.mixer.vsUnit[0].solo       = 0;
            VSQX.mixer.vsUnit[0].pan        = 64;
            VSQX.mixer.vsUnit[0].vol        = 0;

            VSQX.mixer.seUnit = new seUnit { };

            VSQX.mixer.seUnit.inGain     = 0;
            VSQX.mixer.seUnit.sendLevel  = -898;
            VSQX.mixer.seUnit.sendEnable = 0;
            VSQX.mixer.seUnit.mute       = 0;
            VSQX.mixer.seUnit.solo       = 0;
            VSQX.mixer.seUnit.pan        = 64;
            VSQX.mixer.seUnit.vol        = 0;

            VSQX.mixer.karaokeUnit = new karaokeUnit { };

            VSQX.mixer.karaokeUnit.inGain = 0;
            VSQX.mixer.karaokeUnit.mute   = 0;
            VSQX.mixer.karaokeUnit.solo   = 0;
            VSQX.mixer.karaokeUnit.vol    = -129;

            VSQX.masterTrack = new masterTrack { };

            VSQX.masterTrack.seqName    = new XmlDocument().CreateCDataSection("Untitled1");
            VSQX.masterTrack.comment    = new XmlDocument().CreateCDataSection("New VSQ File");
            VSQX.masterTrack.resolution = 480;
            //デフォルトプリメジャーは1小節 
            VSQX.masterTrack.preMeasure = 1;

            VSQX.masterTrack.timeSig = new timeSig[] { new timeSig { } };

            //4分の4拍子とする 
            VSQX.masterTrack.timeSig[0].posMes = 0;
            VSQX.masterTrack.timeSig[0].nume   = 4;
            VSQX.masterTrack.timeSig[0].denomi = 4;

            VSQX.masterTrack.tempo = new tempo[] { new tempo { } };

            //トラック先頭からBPM150で設定する（64分音符を25msにするため）
            VSQX.masterTrack.tempo[0].posTick = 0;
            VSQX.masterTrack.tempo[0].bpm     = 15000;

            VSQX.vsTrack = new vsTrack[] { new vsTrack { } };

            VSQX.vsTrack[0].vsTrackNo = 0;
            VSQX.vsTrack[0].trackName = new XmlDocument().CreateCDataSection("Track");
            VSQX.vsTrack[0].comment   = new XmlDocument().CreateCDataSection("Track");

            VSQX.vsTrack[0].Items = new object[]
            {
                new musicalPart
                {
                    posTick  = 1920,  //デフォルトプリメジャーが1小節なので、1拍480tick*4拍進んだ場所が先頭位置
                    playTime = 76800, //とりあえず40小節（bpm150で64秒）
                    partName = new XmlDocument().CreateCDataSection("NewPart"),
                    comment  = new XmlDocument().CreateCDataSection("New Musical Part"),

                    stylePlugin = new stylePlugin
                    {
                        stylePluginID   = new XmlDocument().CreateCDataSection("ACA9C502-A04B-42b5-B2EB-5CEA36D16FCE"),
                        stylePluginName = new XmlDocument().CreateCDataSection("VOCALOID2 Compatible Style"),
                        version         = new XmlDocument().CreateCDataSection("3.0.0.1")
                    },

                    partStyle = new typeParamAttr[]
                    {
                        new typeParamAttr {id = "accent",   Value = 50},
                        new typeParamAttr {id = "bendDep",  Value = 0},
                        new typeParamAttr {id = "bendLen",  Value = 0},
                        new typeParamAttr {id = "decay",    Value = 0},
                        new typeParamAttr {id = "fallPort", Value = 0},
                        new typeParamAttr {id = "opening",  Value = 127},
                        new typeParamAttr {id = "risePort", Value = 0}
                    },

                    singer = new singer[]
                    {
                        new singer {posTick =0 ,vBS =0 ,vPC=0  }
                    },

                    note = new note[] { }

                }
            };

            VSQX.seTrack = new wavPart[] { };

            VSQX.karaokeTrack = new wavPart[] { };

            VSQX.aux = new aux[] { new aux { } };

            VSQX.aux[0].auxID   = new XmlDocument().CreateCDataSection("AUX_VST_HOST_CHUNK_INFO");
            VSQX.aux[0].content = new XmlDocument().CreateCDataSection("VlNDSwAAAAADAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=");

            return VSQX;

        }

        /// <summary>
        /// トレース情報と音階と音符長さをもとに、vsq3クラスの音符を編集する
        /// </summary>
        /// <param name="I01TIF">入力トレース情報</param>
        /// <param name="noteNumValue">音階（MIDIノート番号）</param>
        /// <param name="posTickValue">音符位置（Tick値）</param>
        /// <param name="durTickValue">音符長さ（Tick値）</param>
        /// <returns>vsq3クラスの音符</returns>
        static note VSQX_note(TIF I01TIF, byte noteNumValue, Double posTickValue, Double durTickValue, TI2VSQXConfig TI2VSQXCFG)
        {
            string PhonemeXSValue;
            string lyricS;

            //空白は音符の長さを64分音符1個分にする(メインロジックと重複しているが念のため入れておく)
            if (I01TIF.PhonemeCurrent == "sil" | I01TIF.PhonemeCurrent == "pau")
                {
                PhonemeXSValue = "Asp";
                lyricS = "Asp";
                durTickValue = 30;
            }
            else
            {
                //トレース情報の音素をもとにボーカロイドで使う発音記号を求める。
                if (TI2VSQXCFG.NoteSplitMode == TI2VSQXConfig.NoteSplitMode_Consonant)
                {
                    PhonemeXSValue = PhonemeXS(I01TIF.MoraCurrent, I01TIF.PhonemeCurrent, I01TIF.PhonemeNext);
                    lyricS = moraToLyric(I01TIF.MoraCurrent, I01TIF.PhonemeCurrent);
                }
                else
                {
                    PhonemeXSValue = PhonemeXS2(I01TIF.MoraCurrent, I01TIF.PhonemeCurrent, I01TIF.PhonemeNext, I01TIF.PhonemePrev);
                    lyricS = moraToLyric2(I01TIF.MoraCurrent, I01TIF.PhonemeCurrent, I01TIF.PhonemePrev);
                }
                //音素が「ン」の場合は次のモーラの音素を参照して発音記号を求める。
                if (I01TIF.MoraCurrent == "ン")
                {
                    PhonemeXSValue = PhonemeXSN(I01TIF.PhonemeNext, I01TIF.PhonemeAfterNext);
                }
            };

            var nt = new note
            {
                posTick = (int)posTickValue,
                durTick = (int)durTickValue,
                noteNum = noteNumValue,
                velocity = 64,
                lyric = new XmlDocument().CreateCDataSection(lyricS),

                phnms = new typePhonemes
                {
                    //子音部分の歌詞がトレース情報の音素情報であり発音記号に変換できないため、プロテクトをかけて発音記号が崩れないようにする。
                    @lock = 1,
                    lockSpecified = true,
                    Value = PhonemeXSValue
                },

                noteStyle = new noteStyle
                {
                    attr = new typeParamAttr[]
                    {
                        new typeParamAttr { id = "accent",   Value = 50},
                        new typeParamAttr { id = "bendDep",  Value = 0},
                        new typeParamAttr { id = "bendLen",  Value = 0},
                        new typeParamAttr { id = "decay",    Value = 0},
                        new typeParamAttr { id = "fallPort", Value = 0},
                        new typeParamAttr { id = "opening",  Value = 127},
                        new typeParamAttr { id = "risePort", Value = 0},
                        new typeParamAttr { id = "vibLen",   Value = 0},
                        new typeParamAttr { id = "vibType",  Value = 0}
                    },
                    seqAttr = new seqAttr[] { }
                }
            };
            return nt; 
        }

        /// <summary>
        /// 各種動作設定を扱うクラス
        /// </summary>
        public class TI2VSQXConfig
        {
            public string compID;
            public string vVoiceName;
            public Double BaseNoteNum;
            public Double MiddleNoteNum;
            public Double TopNoteNum;
            public Double NaturalEndNoteNum;
            public Double BottomNoteNum;
            public Double QuestionNoteNum;
            public Double NoteSplitMode;

            public const Double NoteSplitMode_Consonant = 1;    //子音単独音素を分割して前に置く(通常方式)  
            public const Double NoteSplitMode_Vowel = 2;        //母音単独音素を分割して後ろに置く(UTAUなど他の歌声合成方式変換用)

            /// <summary>
            /// 各種動作設定を扱うクラスのコンストラクタ
            /// </summary>
            /// <param name="sConfigpass">設定ファイルのパス</param>
            public TI2VSQXConfig(string sConfigpass)
            {
                //デフォルト値設定 
                compID     = "BCNFCY43LB2LZCD4";
                vVoiceName = "MIKU_V4X_Original_EVEC";
                BaseNoteNum       = 62.00;
                MiddleNoteNum     = 67.00;
                TopNoteNum        = 70.00;
                NaturalEndNoteNum = 68.00;
                BottomNoteNum     = 60.00;
                QuestionNoteNum   = 66.00;
                NoteSplitMode     =  1.00;

                if (!System.IO.File.Exists(sConfigpass))
                {
                    return;
                };

                System.IO.StreamReader srConfig = new System.IO.StreamReader(sConfigpass, Encoding.GetEncoding("SHIFT_JIS"));

                string CFG_Line = srConfig.ReadLine();
                string[] item;

                while (srConfig.EndOfStream == false)
                {
                    item = CFG_Line.Split('=');
                    item[0] = item[0].Trim();
                    item[1] = item[1].Trim();
                    switch (item[0])
                    {
                        case "compID":
                            compID = item[1];
                            break;
                        case "vVoiceName":
                            vVoiceName = item[1];
                            break;
                        case "BaseNoteNum":
                            BaseNoteNum = Convert.ToInt32(item[1]);
                            if ((0 <= BaseNoteNum) & (BaseNoteNum <= 127))
                            {
                                BaseNoteNum = BaseNoteNum * 1.00;
                            }
                            else
                            {
                                BaseNoteNum = 62.00;
                            }
                            break;
                        case "MiddleNoteNum":
                            MiddleNoteNum = Convert.ToInt32(item[1]);
                            if ((0 <= MiddleNoteNum) & (MiddleNoteNum <= 127))
                            {
                                MiddleNoteNum = MiddleNoteNum * 1.00;
                            }
                            else
                            {
                                MiddleNoteNum = 67.00;
                            }
                            break;
                        case "TopNoteNum":
                            TopNoteNum = Convert.ToInt32(item[1]);
                            if ((0 <= TopNoteNum) & (TopNoteNum <= 127))
                            {
                                TopNoteNum = TopNoteNum * 1.00;
                            }
                            else
                            {
                                TopNoteNum = 70.00;
                            }
                            break;
                        case "NaturalEndNoteNum":
                            NaturalEndNoteNum = Convert.ToInt32(item[1]);
                            if ((0 <= NaturalEndNoteNum) & (NaturalEndNoteNum <= 127))
                            {
                                NaturalEndNoteNum = NaturalEndNoteNum * 1.00;
                            }
                            else
                            {
                                NaturalEndNoteNum = 68.00;
                            }
                            break;
                        case "BottomNoteNum":
                            BottomNoteNum = Convert.ToInt32(item[1]);
                            if ((0 <= BottomNoteNum) & (BottomNoteNum <= 127))
                            {
                                BottomNoteNum = BottomNoteNum * 1.00;
                            }
                            else
                            {
                                BottomNoteNum = 60.00;
                            }
                            break;
                        case "QuestionNoteNum":
                            QuestionNoteNum = Convert.ToInt32(item[1]);
                            if ((0 <= QuestionNoteNum) & (QuestionNoteNum <= 127))
                            {
                                QuestionNoteNum = QuestionNoteNum * 1.00;
                            }
                            else
                            {
                                QuestionNoteNum = 64.00;
                            }
                            break;
                        case "NoteSplitMode":
                            NoteSplitMode = Convert.ToInt32(item[1]);
                            if ((1 <= NoteSplitMode) & (NoteSplitMode <= 2))
                            {
                                NoteSplitMode = NoteSplitMode * 1.00;
                            }
                            else
                            {
                                NoteSplitMode = 1.00;
                            }
                            break;
                    }
                    CFG_Line = srConfig.ReadLine();
                }
            }
        }

        /// <summary>
        /// トレース情報で扱う秒（0.1マイクロ秒）をTick値に変換する。BPM150で64分音符(25ms)単位にする。
        /// </summary>
        /// <param name="MicroSecond">トレース情報で扱う秒（0.1マイクロ秒）</param>
        /// <returns>Tick値変換した値</returns>
        public static  double MicroSecondToTick(double MicroSecond)
        {
            //秒数を64分音符の個数に変換する。BPM150なので64分音符1個は25ms。
            Decimal MicroSecondD = (Decimal)MicroSecond;
            MicroSecondD = MicroSecondD / 25m;
            MicroSecondD = MicroSecondD / 10000m;
            MicroSecondD = Math.Round(MicroSecondD);
            MicroSecondD = MicroSecondD * 30m;
            return (double)MicroSecondD;
        }

        /// <summary>
        /// メインプログラム
        /// </summary>
        static void Main(string[] args)
        {

            // 各種ファイルのパスをコマンドライン引数から取得する
            string sConfigPass = args[0];  //設定情報
            string sTIFPass    = args[1];  //入力するトレース情報
            string sTIFXAPass  = args[2];  //入力したトレース情報のリストをシリアライズしたもの
            string sTIFXPass   = args[3];  //入力したトレース情報のリストをシリアライズしたもの
            string sVSQXPass   = args[4];  //出力VSQXファイル

            if (!System.IO.File.Exists(sTIFPass))
            {
                Console.WriteLine("'" + sTIFPass + "'は存在しません。");
                return;
            }

            // 設定情報を読んで設定情報を取得する
            var TI2VSQXCFG = new TI2VSQXConfig(sConfigPass);

            // vsq3オブジェクトの初期設定
            var VSQX = VSQX_Init(TI2VSQXCFG.compID, TI2VSQXCFG.vVoiceName);

            // 音符情報と、対応するトレース情報リストのレコード情報を格納する配列を暫定的に要素10000個で設定
            var noteArray = new note[10000];
            var TIFArray  = new TIF[10000];

            // トレース情報を格納するリストを設定
            var T01TIF = new List<TIF>();

            // 処理全体単位の変数初期設定
            int noteIdx = 0;
            byte noteNumValue = 0;
            Double posTickValue = 0;
            Double durTickValue = 0;

            System.IO.StreamReader srTIF = new System.IO.StreamReader(sTIFPass, Encoding.GetEncoding("SHIFT_JIS"));

            TIF I01TIF;
            string I01_Line = srTIF.ReadLine();

            //トレース情報をテキスト解析情報のタイトルまで読み飛ばす
            while ((srTIF.EndOfStream == false) & (I01_Line != "[Text analysis result]"))
            {
                I01_Line = srTIF.ReadLine();
            }

            //タイトルは読み飛ばす
            if (srTIF.EndOfStream == false)
            {
                I01_Line = srTIF.ReadLine();
            }

            TIF_TextAnalysis   I01TIFTXA;
            var T01TIFTXA = new List<TIF_TextAnalysis>();

            //テキスト解析情報を読み終わるで処理する
            while ((srTIF.EndOfStream == false) & (I01_Line != ""))
            {
                I01TIFTXA = new TIF_TextAnalysis(I01_Line);
                //テキスト解析情報のシリアライズ用にリストに追加する
                T01TIFTXA.Add(I01TIFTXA);
                I01_Line = srTIF.ReadLine();
            }

            //トレース情報をラベル情報のタイトルまで読み飛ばす
            while ((srTIF.EndOfStream == false) & (I01_Line != "[Output label]"))
            {
                I01_Line = srTIF.ReadLine();
            }

            //タイトルは読み飛ばす
            if (srTIF.EndOfStream == false)
            {
                I01_Line = srTIF.ReadLine();
            }

            //出力ラベル情報を読み終わるで処理する
            while ((srTIF.EndOfStream == false) & (I01_Line != ""))
            {
                I01TIF = new TIF(I01_Line);

                //呼気段落ごとの処理用変数を初期化する
                int noteIdxInUtterance = 0;
                int MoraLocStart     = 0;
                int MoraNoteIdxStart = 0;
                String Mora1      = "";
                int MoraLoc1      = 0;
                int MoraNoteIdx1  = 0;
                String Mora2      = "";
                int MoraLoc2      = 0;
                int MoraNoteIdx2  = 0;
                String Mora3      = "";
                int MoraLoc3      = 0;
                int MoraNoteIdx3  = 0;
                String Mora4      = "";
                int MoraLoc4      = 0;
                int MoraNoteIdx4  = 0;
                String MoraTop     = "";
                int MoraLocTop     = 0;
                int MoraNoteIdxTop = 0;
                String MoraDownStart     = "";
                int MoraLocDownStart     = 0;
                int MoraNoteIdxDownStart = 0;
                String MoraEnd     = "";
                int MoraLocEnd     = 0;
                int MoraNoteIdxEnd = 0;

                //音符位置をTick値に変換する。
                posTickValue = MicroSecondToTick(I01TIF.PhonemeFrom);

                string OldMoraDiffAccent  = I01TIF.MoraDiffAccent;
                string OldMoraPosForward  = I01TIF.MoraPosForward;
                string OldMoraPosBackward = I01TIF.MoraPosBackward;

                //呼気段落末までの繰り返し
                while (I01TIF.BreathPosInUtteranceForward != "xx")
                {

                    //音符長さを求める
                    durTickValue = MicroSecondToTick(I01TIF.PhonemeTo - I01TIF.PhonemeFrom);

                    //音符を配列に追加
                    noteArray[noteIdx] = VSQX_note(I01TIF, (byte)TI2VSQXCFG.BaseNoteNum, posTickValue, durTickValue, TI2VSQXCFG);
                    TIFArray[noteIdx]  = I01TIF;

                    //１モーラ目～４モーラ目までの開始位置の音符の位置情報を取得する
                    if (Mora1 == "")
                    {
                        Mora1        = I01TIF.MoraCurrent;
                        MoraLoc1     = (int)posTickValue;
                        MoraNoteIdx1 = noteIdx;
                    }

                    //モーラが変わったか判定する
                    if ((OldMoraDiffAccent != I01TIF.MoraDiffAccent) |
                        (OldMoraPosForward != I01TIF.MoraPosForward) |
                        (OldMoraPosBackward != I01TIF.MoraPosBackward))
                    {
                        if (Mora2 == "")
                        {
                            Mora2        = I01TIF.MoraCurrent;
                            MoraLoc2     = (int)posTickValue;
                            MoraNoteIdx2 = noteIdx;
                        }else if (Mora3 == "")
                        {
                            Mora3        = I01TIF.MoraCurrent;
                            MoraLoc3     = (int)posTickValue;
                            MoraNoteIdx3 = noteIdx;
                        }else if (Mora4 == "")
                        {
                            Mora4        = I01TIF.MoraCurrent;
                            MoraLoc4     = (int)posTickValue;
                            MoraNoteIdx4 = noteIdx;
                        }
                    }
                    OldMoraDiffAccent = I01TIF.MoraDiffAccent;
                    OldMoraPosForward = I01TIF.MoraPosForward;
                    OldMoraPosBackward = I01TIF.MoraPosBackward;

                    //最初のアクセント核モーラ開始位置の音符の位置情報を取得する（下降開始位置への調整は後で行う）
                    if (MoraDownStart == "")
                    {
                        if (I01TIF.MoraDiffAccent == "0")
                        {
                            MoraDownStart        = I01TIF.MoraCurrent;
                            MoraNoteIdxDownStart = noteIdx;
                        }
                    }

                    //呼気段落末の音符の位置情報を取得する
                    MoraEnd        = I01TIF.MoraCurrent;
                    MoraLocEnd     = (int)posTickValue + (int)durTickValue ;
                    MoraNoteIdxEnd = noteIdx;

                    //次の音符の開始位置は音符長さ分進んだ直後にする（音符の重複回避のため）
                    posTickValue = posTickValue + durTickValue;

                    //配列の添字を次に進める
                    noteIdx++;
                    noteIdxInUtterance++;

                    //トレース情報のシリアライズ用にリストに追加する
                    T01TIF.Add(I01TIF);

                    //次のトレース情報レコードを読む
                    I01_Line = srTIF.ReadLine();
                    I01TIF   = new TIF(I01_Line);
                }

                //文字数が短いなど、上記ロジックでアクセント下降開始位置が求められない場合は句末を下降開始位置とする。
                if (MoraDownStart == "")
                {
                    MoraNoteIdxDownStart = MoraNoteIdxEnd;
                }

                //呼気段落内に音素がある場合は(「音素がない」のは先頭空白のみのはず）
                if (noteIdxInUtterance != 0)
                {
                    //アクセント核がある場合は下降開始位置を求める。
                    //基本はアクセント核の次のモーラ開始位置だが、
                    //次のモーラの音素数が１つ（あ段）の場合はアクセント核の最終音素を開始位置とする
                    if (MoraDownStart != "")
                    {
                        //次の音素がある場合
                        if ((MoraNoteIdxDownStart + 1) <= MoraNoteIdxEnd)
                        {
                            //次の音素がアクセント核のモーラの時は
                            if (T01TIF[MoraNoteIdxDownStart + 1].MoraDiffAccent == "0")
                            {
                                //次の次の音素がある場合
                                if ((MoraNoteIdxDownStart + 2) <= MoraNoteIdxEnd)
                                {
                                    //次の次の音素が同じアクセント句の次のモーラの場合は
                                    if (T01TIF[MoraNoteIdxDownStart + 2].MoraDiffAccent == "1")
                                    {
                                        MoraNoteIdxDownStart = MoraNoteIdxDownStart + 2;
                                        MoraDownStart = T01TIF[MoraNoteIdxDownStart].MoraCurrent;
                                        //音符位置をtick値に変換する。
                                        MoraLocDownStart = (int)MicroSecondToTick(T01TIF[MoraNoteIdxDownStart].PhonemeFrom);
                                    }
                                    //次の次の音素が別のアクセント句のモーラの場合は
                                    else
                                    {
                                        MoraNoteIdxDownStart = MoraNoteIdxDownStart + 1;
                                        MoraDownStart = T01TIF[MoraNoteIdxDownStart].MoraCurrent;
                                        //音符位置をtick値に変換する。
                                        MoraLocDownStart = (int)MicroSecondToTick(T01TIF[MoraNoteIdxDownStart].PhonemeFrom);
                                    }
                                }
                                //次の次の音素がない場合
                                else
                                {
                                    MoraNoteIdxDownStart = MoraNoteIdxDownStart + 1;
                                    MoraDownStart = T01TIF[MoraNoteIdxDownStart].MoraCurrent;
                                    //音符位置をtick値に変換する。
                                    MoraLocDownStart = (int)MicroSecondToTick(T01TIF[MoraNoteIdxDownStart].PhonemeFrom);
                                }
                            }
                            //次の音素がアクセント核のモーラでない時は
                            else
                            {
                                //次の音素が同じアクセント句の次のモーラの場合は
                                if (T01TIF[MoraNoteIdxDownStart + 1].MoraDiffAccent == "1")
                                {
                                    MoraNoteIdxDownStart = MoraNoteIdxDownStart + 1;
                                    MoraDownStart = T01TIF[MoraNoteIdxDownStart].MoraCurrent;
                                    //音符位置をtick値に変換する。
                                    MoraLocDownStart = (int)MicroSecondToTick(T01TIF[MoraNoteIdxDownStart].PhonemeFrom);
                                }
                                //次の音素が別のアクセント句のモーラの場合は
                                else
                                {
                                    //先頭がアクセント核でかつ音素数が１つの場合の下降開始位置としてアクセント核の音素終了位置を設定する
                                    //音符位置をtick値に変換する。
                                    MoraLocDownStart = (int)MicroSecondToTick(T01TIF[MoraNoteIdxDownStart].PhonemeTo);
                                }
                            }
                        }
                        //次の音素がない場合
                        else
                        {
                            //下降開始位置としてアクセント核の音素終了位置を設定する
                            //音符位置をtick値に変換する。
                            MoraLocDownStart = (int)MicroSecondToTick(T01TIF[MoraNoteIdxDownStart].PhonemeTo);
                        }
                    }

                    //句頭上昇位置を求める。基本は２モーラ目直後の音符
                    MoraTop          = Mora2;
                    MoraLocTop       = MoraLoc3;
                    MoraNoteIdxTop   = MoraNoteIdx3;
                    MoraLocStart     = MoraLoc2;
                    MoraNoteIdxStart = MoraNoteIdx2;

                    //３モーラ目がない場合は１モーラ目直後にする
                    if (Mora3 == "")
                    {
                        MoraTop = Mora1;
                        MoraLocTop = MoraLoc2;
                        MoraNoteIdxTop = MoraNoteIdx2;
                        MoraLocStart = MoraLoc1;
                        MoraNoteIdxStart = MoraNoteIdx1;
                    }

                    //２モーラ目がない場合は１モーラ目にする
                    if (Mora2 == "")
                    {
                        MoraTop = Mora1;
                        MoraLocTop = MoraLoc1;
                        MoraNoteIdxTop = MoraNoteIdx1;
                        MoraLocStart = MoraLoc1;
                        MoraNoteIdxStart = MoraNoteIdx1;
                    }

                    //２モーラ目が「ん」あるいは「ー」の場合は１モーラ目直後にずらす
                    if (MoraTop == "ン" | MoraTop == "ー")
                    {
                        MoraTop          = Mora1;
                        MoraLocTop       = MoraLoc2;
                        MoraNoteIdxTop   = MoraNoteIdx2;
                        MoraLocStart     = MoraLoc1;
                        MoraNoteIdxStart = MoraNoteIdx1;
                    }

                    //２モーラ目が「ッ」の場合は３モーラ目直後にずらす
                    if (MoraTop == "ッ")
                    {
                        MoraTop          = Mora3;
                        MoraLocTop       = MoraLoc4;
                        MoraNoteIdxTop   = MoraNoteIdx4;
                        MoraLocStart     = MoraLoc3;
                        MoraNoteIdxStart = MoraNoteIdx3;
                    }

                    //１モーラ目がアクセント核の場合は１モーラ目直後にずらす
                    if (MoraNoteIdxDownStart <= MoraNoteIdx2)
                    {
                        MoraTop          = Mora1;
                        MoraLocTop       = MoraLoc2;
                        MoraNoteIdxTop   = MoraNoteIdx2;
                        MoraLocStart     = MoraLoc1;
                        MoraNoteIdxStart = MoraNoteIdx1;
                    }


                    //句頭上昇部分の音階を設定する
                    noteArray[MoraNoteIdxTop].noteNum     = (byte)TI2VSQXCFG.TopNoteNum;
                    if (MoraNoteIdxTop != MoraNoteIdx1)
                    {
                        noteArray[MoraNoteIdxTop - 1].noteNum = (byte)TI2VSQXCFG.MiddleNoteNum;
                    } 

                    //音階頂点位置から呼気段落末(音の高さは自然下降のみとする)までの傾きを求める
                    Decimal SlopeNatural = (Decimal)(TI2VSQXCFG.NaturalEndNoteNum - TI2VSQXCFG.TopNoteNum) / (MoraLocEnd - MoraLocTop);

                    //アクセント核からあとの時間の合計を求める
                    Decimal SlopeLen = 0;
                    Decimal SlopeEnd = 0;
                    for (int i = MoraNoteIdxDownStart; i <= MoraNoteIdxEnd; i++)
                    {
                        int MoraDiff = Convert.ToInt32(TIFArray[i].MoraDiffAccent);
                        if (MoraDiff > 0)
                        {
                            SlopeLen = SlopeLen + noteArray[i].durTick;
                            //アクセント核からあとの下降時の傾きを求める
                            SlopeEnd = (Decimal)(TI2VSQXCFG.BottomNoteNum - TI2VSQXCFG.NaturalEndNoteNum) / SlopeLen;
                        }
                    }

                    Decimal DownPos = 0;
                    Decimal x1 = 0;
                    Decimal x2 = 0;
                    Decimal x3 = 0;
                    for (int i = MoraNoteIdxTop + 1; i <= MoraNoteIdxEnd; i++)
                    {
                        //自然下降のみとして、音階頂点位置からの音階下げ幅を求める
                        x1 = (noteArray[i].posTick - noteArray[MoraNoteIdxTop].posTick);
                        x1 = x1 * SlopeNatural;

                        //アクセント核がある場合は
                        if (MoraDownStart != "")
                        {
                            int MoraDiff = Convert.ToInt32(TIFArray[i].MoraDiffAccent);
                            //アクセント核からあとは句末への下降部分の音階下げ幅を求める
                            if (MoraDiff > 0)
                            {
                                x2 = DownPos * SlopeEnd;
                                DownPos = DownPos + noteArray[i].durTick;
                            }
                        }

                        //自然下降の下げ幅とアクセント核から後の下降時の下げ幅の合算を音階下げ幅とする
                        x3 = (Decimal)TI2VSQXCFG.TopNoteNum +  x1 + x2 ;
                        noteArray[i].noteNum = (byte)Math.Round(x3);
                    }

                    //呼気段落末直後の空白音符の音階を設定する。
                    if (I01TIF.IsInterrogativePrevAcc == "0")
                    {
                        //アクセント核を持たない場合は自然下降した先の音程とする
                        if (MoraDownStart == "")
                        {
                            noteNumValue = (byte)TI2VSQXCFG.NaturalEndNoteNum;
                        }
                        //アクセント核を持つ場合 
                        else
                        {
                            //アクセント核で終わっている場合はアクセント核の音程を維持する
                            if (T01TIF[noteIdx - 1].MoraDiffAccent == "0")
                            {
                                noteNumValue = noteArray[noteIdx - 1].noteNum;
                            }
                            //アクセント核で終わってない場合は文末の音程に下げていく
                            else
                            {
                                noteNumValue = (byte)TI2VSQXCFG.BottomNoteNum;
                            }
                        }
                    }
                    //疑問文は音階を上げる
                    else
                    {
                        noteNumValue = (byte)TI2VSQXCFG.QuestionNoteNum;
                    }

                    //呼気段落末直後の空白音符の長さは64分音符1個分
                    durTickValue = 30;

                    //呼気段落末直後の空白音符を追加する
                    noteArray[noteIdx] = VSQX_note(I01TIF, noteNumValue, posTickValue, durTickValue, TI2VSQXCFG);
                    noteIdx++;
                    T01TIF.Add(I01TIF);

                 }
                 //次のトレース情報を読む
                 I01_Line = srTIF.ReadLine();

            }
            srTIF.Close();

            // 音階を設定した音符情報をvsq3オブジェクトに設定する
            ((musicalPart)(VSQX.vsTrack[0].Items[0])).note = noteArray;

            // パート長さは最後の音符位置から一拍あと
            ((musicalPart)(VSQX.vsTrack[0].Items[0])).playTime = (int)posTickValue + 30 + 480;

            // トレース情報のテキスト解析情報リストをシリアライズする
            var xmlSerializer1 = new XmlSerializer(typeof(List<TIF_TextAnalysis>));
            using (var streamWriter = new StreamWriter(sTIFXAPass, false, Encoding.UTF8))
            {
                xmlSerializer1.Serialize(streamWriter, T01TIFTXA);
                streamWriter.Flush();
            }

            // トレース情報の出力ラベル情報リストをシリアライズする
            var xmlSerializer2 = new XmlSerializer(typeof(List<TIF>));
            using (var streamWriter = new StreamWriter(sTIFXPass, false, Encoding.UTF8))
            {
                xmlSerializer2.Serialize(streamWriter, T01TIF);
                streamWriter.Flush();
            }

            // vsq3オブジェクトをシリアライズする
            var xmlSerializer3 = new XmlSerializer(typeof(vsq3));
            using (var streamWriter = new StreamWriter(sVSQXPass, false, Encoding.UTF8))
            {
                xmlSerializer3.Serialize(streamWriter, VSQX);
                streamWriter.Flush();
            }

        }
    }
}