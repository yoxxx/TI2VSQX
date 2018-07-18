using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using static System.Console;

namespace TI2VSQX
{

    class Program
    {
        //モーラ情報と音素をもとに、VOCALOIDの発音記号(X-SAMPA)を求めるためのテーブルの構造体　
        public struct jpcommonMora
        {
            public string moraValue;
            public string PhonemeHead;
            public string PhonemeTail;
            public string PhonemeXSHead;
            public string PhonemeXSTail;
            public string PhonemeXSN;
        };

        //モーラ情報と音素をもとに、VOCALOIDの発音記号(X-SAMPA)を求めるためのテーブル　
        static public jpcommonMora[] jpcommonMoraArray = new jpcommonMora[]
        {
            new jpcommonMora { moraValue = "ヴョ", PhonemeHead = "by", PhonemeTail = "o", PhonemeXSHead = "b"   , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴュ", PhonemeHead = "by", PhonemeTail = "u", PhonemeXSHead = "b"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴャ", PhonemeHead = "by", PhonemeTail = "a", PhonemeXSHead = "b"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴォ", PhonemeHead = "v" , PhonemeTail = "o", PhonemeXSHead = "b"   , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴェ", PhonemeHead = "v" , PhonemeTail = "e", PhonemeXSHead = "b"   , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴィ", PhonemeHead = "v" , PhonemeTail = "i", PhonemeXSHead = "b"   , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴァ", PhonemeHead = "v" , PhonemeTail = "a", PhonemeXSHead = "b"   , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヴ"  , PhonemeHead = "v" , PhonemeTail = "u", PhonemeXSHead = "b"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ン"  , PhonemeHead = "N" , PhonemeTail = "" , PhonemeXSHead = "N\\" , PhonemeXSTail = ""  , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヲ"  , PhonemeHead = "o" , PhonemeTail = "" , PhonemeXSHead = "o"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヱ"  , PhonemeHead = "e" , PhonemeTail = "" , PhonemeXSHead = "w e" , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヰ"  , PhonemeHead = "i" , PhonemeTail = "" , PhonemeXSHead = "w i" , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ワ"  , PhonemeHead = "w" , PhonemeTail = "a", PhonemeXSHead = "w"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヮ"  , PhonemeHead = "w" , PhonemeTail = "a", PhonemeXSHead = "w"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ロ"  , PhonemeHead = "r" , PhonemeTail = "o", PhonemeXSHead = "4"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "レ"  , PhonemeHead = "r" , PhonemeTail = "e", PhonemeXSHead = "4"   , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ル"  , PhonemeHead = "r" , PhonemeTail = "u", PhonemeXSHead = "4"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "リョ", PhonemeHead = "ry", PhonemeTail = "o", PhonemeXSHead = "4'"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "リュ", PhonemeHead = "ry", PhonemeTail = "u", PhonemeXSHead = "4'"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "リャ", PhonemeHead = "ry", PhonemeTail = "a", PhonemeXSHead = "4'"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "リェ", PhonemeHead = "ry", PhonemeTail = "e", PhonemeXSHead = "4'"  , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "リ"  , PhonemeHead = "r" , PhonemeTail = "i", PhonemeXSHead = "4'"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ラ"  , PhonemeHead = "r" , PhonemeTail = "a", PhonemeXSHead = "4"   , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヨ"  , PhonemeHead = "y" , PhonemeTail = "o", PhonemeXSHead = "j"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ョ"  , PhonemeHead = "y" , PhonemeTail = "o", PhonemeXSHead = "j"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ユ"  , PhonemeHead = "y" , PhonemeTail = "u", PhonemeXSHead = "j"   , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ュ"  , PhonemeHead = "y" , PhonemeTail = "u", PhonemeXSHead = "j"   , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヤ"  , PhonemeHead = "y" , PhonemeTail = "a", PhonemeXSHead = "j"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ャ"  , PhonemeHead = "y" , PhonemeTail = "a", PhonemeXSHead = "j"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "モ"  , PhonemeHead = "m" , PhonemeTail = "o", PhonemeXSHead = "m"   , PhonemeXSTail = "o" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "メ"  , PhonemeHead = "m" , PhonemeTail = "e", PhonemeXSHead = "m"   , PhonemeXSTail = "e" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ム"  , PhonemeHead = "m" , PhonemeTail = "u", PhonemeXSHead = "m"   , PhonemeXSTail = "M" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ミョ", PhonemeHead = "my", PhonemeTail = "o", PhonemeXSHead = "m'"  , PhonemeXSTail = "o" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ミュ", PhonemeHead = "my", PhonemeTail = "u", PhonemeXSHead = "m'"  , PhonemeXSTail = "M" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ミャ", PhonemeHead = "my", PhonemeTail = "a", PhonemeXSHead = "m'"  , PhonemeXSTail = "a" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ミェ", PhonemeHead = "my", PhonemeTail = "e", PhonemeXSHead = "m'"  , PhonemeXSTail = "e" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ミ"  , PhonemeHead = "m" , PhonemeTail = "i", PhonemeXSHead = "m'"  , PhonemeXSTail = "i" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "マ"  , PhonemeHead = "m" , PhonemeTail = "a", PhonemeXSHead = "m"   , PhonemeXSTail = "a" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ポ"  , PhonemeHead = "p" , PhonemeTail = "o", PhonemeXSHead = "p"   , PhonemeXSTail = "o" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ボ"  , PhonemeHead = "b" , PhonemeTail = "o", PhonemeXSHead = "b"   , PhonemeXSTail = "o" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ホ"  , PhonemeHead = "h" , PhonemeTail = "o", PhonemeXSHead = "h"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ペ"  , PhonemeHead = "p" , PhonemeTail = "e", PhonemeXSHead = "p"   , PhonemeXSTail = "e" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ベ"  , PhonemeHead = "b" , PhonemeTail = "e", PhonemeXSHead = "b"   , PhonemeXSTail = "e" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ヘ"  , PhonemeHead = "h" , PhonemeTail = "e", PhonemeXSHead = "h"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "プ"  , PhonemeHead = "p" , PhonemeTail = "u", PhonemeXSHead = "p"   , PhonemeXSTail = "M" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ブ"  , PhonemeHead = "b" , PhonemeTail = "u", PhonemeXSHead = "b"   , PhonemeXSTail = "M" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "フォ", PhonemeHead = "f" , PhonemeTail = "o", PhonemeXSHead = "p\\" , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "フェ", PhonemeHead = "f" , PhonemeTail = "e", PhonemeXSHead = "p\\" , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "フィ", PhonemeHead = "f" , PhonemeTail = "i", PhonemeXSHead = "p\\'", PhonemeXSTail = "i" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ファ", PhonemeHead = "f" , PhonemeTail = "a", PhonemeXSHead = "p\\" , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "フ"  , PhonemeHead = "f" , PhonemeTail = "u", PhonemeXSHead = "p'"  , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ピョ", PhonemeHead = "py", PhonemeTail = "o", PhonemeXSHead = "p'"  , PhonemeXSTail = "o" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ピュ", PhonemeHead = "py", PhonemeTail = "u", PhonemeXSHead = "p'"  , PhonemeXSTail = "M" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ピャ", PhonemeHead = "py", PhonemeTail = "a", PhonemeXSHead = "p'"  , PhonemeXSTail = "a" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ピェ", PhonemeHead = "py", PhonemeTail = "e", PhonemeXSHead = "p'"  , PhonemeXSTail = "e" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ピ"  , PhonemeHead = "p" , PhonemeTail = "i", PhonemeXSHead = "p'"  , PhonemeXSTail = "i" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ビョ", PhonemeHead = "by", PhonemeTail = "o", PhonemeXSHead = "b'"  , PhonemeXSTail = "o" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ビュ", PhonemeHead = "by", PhonemeTail = "u", PhonemeXSHead = "b'"  , PhonemeXSTail = "M" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ビャ", PhonemeHead = "by", PhonemeTail = "a", PhonemeXSHead = "b'"  , PhonemeXSTail = "a" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ビェ", PhonemeHead = "by", PhonemeTail = "e", PhonemeXSHead = "b'"  , PhonemeXSTail = "e" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ビ"  , PhonemeHead = "b" , PhonemeTail = "i", PhonemeXSHead = "b'"  , PhonemeXSTail = "i" , PhonemeXSN = "m'" },
            new jpcommonMora { moraValue = "ヒョ", PhonemeHead = "hy", PhonemeTail = "o", PhonemeXSHead = "C"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヒュ", PhonemeHead = "hy", PhonemeTail = "u", PhonemeXSHead = "C"   , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヒャ", PhonemeHead = "hy", PhonemeTail = "a", PhonemeXSHead = "C"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヒェ", PhonemeHead = "hy", PhonemeTail = "e", PhonemeXSHead = "C"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ヒ"  , PhonemeHead = "h" , PhonemeTail = "i", PhonemeXSHead = "C"   , PhonemeXSTail = "i" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "パ"  , PhonemeHead = "p" , PhonemeTail = "a", PhonemeXSHead = "p"   , PhonemeXSTail = "a" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "バ"  , PhonemeHead = "b" , PhonemeTail = "a", PhonemeXSHead = "b"   , PhonemeXSTail = "a" , PhonemeXSN = "m" },
            new jpcommonMora { moraValue = "ハ"  , PhonemeHead = "h" , PhonemeTail = "a", PhonemeXSHead = "h"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ノ"  , PhonemeHead = "n" , PhonemeTail = "o", PhonemeXSHead = "n"   , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ネ"  , PhonemeHead = "n" , PhonemeTail = "e", PhonemeXSHead = "n"   , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヌ"  , PhonemeHead = "n" , PhonemeTail = "u", PhonemeXSHead = "n"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ニョ", PhonemeHead = "ny", PhonemeTail = "o", PhonemeXSHead = "J"   , PhonemeXSTail = "o" , PhonemeXSN = "J" },
            new jpcommonMora { moraValue = "ニュ", PhonemeHead = "ny", PhonemeTail = "u", PhonemeXSHead = "J"   , PhonemeXSTail = "M" , PhonemeXSN = "J" },
            new jpcommonMora { moraValue = "ニャ", PhonemeHead = "ny", PhonemeTail = "a", PhonemeXSHead = "J"   , PhonemeXSTail = "a" , PhonemeXSN = "J" },
            new jpcommonMora { moraValue = "ニェ", PhonemeHead = "ny", PhonemeTail = "e", PhonemeXSHead = "J"   , PhonemeXSTail = "e" , PhonemeXSN = "J" },
            new jpcommonMora { moraValue = "ニ"  , PhonemeHead = "n" , PhonemeTail = "i", PhonemeXSHead = "J"   , PhonemeXSTail = "i" , PhonemeXSN = "J" },
            new jpcommonMora { moraValue = "ナ"  , PhonemeHead = "n" , PhonemeTail = "a", PhonemeXSHead = "n"   , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ドゥ", PhonemeHead = "d" , PhonemeTail = "u", PhonemeXSHead = "d"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ド"  , PhonemeHead = "d" , PhonemeTail = "o", PhonemeXSHead = "d"   , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "トゥ", PhonemeHead = "t" , PhonemeTail = "u", PhonemeXSHead = "t"   , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ト"  , PhonemeHead = "t" , PhonemeTail = "o", PhonemeXSHead = "t"   , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "デョ", PhonemeHead = "dy", PhonemeTail = "o", PhonemeXSHead = "d'"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "デュ", PhonemeHead = "dy", PhonemeTail = "u", PhonemeXSHead = "d'"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "デャ", PhonemeHead = "dy", PhonemeTail = "a", PhonemeXSHead = "d'"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ディ", PhonemeHead = "d" , PhonemeTail = "i", PhonemeXSHead = "d'"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "デ"  , PhonemeHead = "d" , PhonemeTail = "e", PhonemeXSHead = "d"   , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "テョ", PhonemeHead = "ty", PhonemeTail = "o", PhonemeXSHead = "t'"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "テュ", PhonemeHead = "ty", PhonemeTail = "u", PhonemeXSHead = "t'"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "テャ", PhonemeHead = "ty", PhonemeTail = "a", PhonemeXSHead = "t'"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ティ", PhonemeHead = "t" , PhonemeTail = "i", PhonemeXSHead = "t'"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "テ"  , PhonemeHead = "t" , PhonemeTail = "e", PhonemeXSHead = "t"   , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヅ"  , PhonemeHead = "z" , PhonemeTail = "u", PhonemeXSHead = "dz"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ツォ", PhonemeHead = "ts", PhonemeTail = "o", PhonemeXSHead = "ts"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ツェ", PhonemeHead = "ts", PhonemeTail = "e", PhonemeXSHead = "ts"  , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ツィ", PhonemeHead = "ts", PhonemeTail = "i", PhonemeXSHead = "ts"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ツァ", PhonemeHead = "ts", PhonemeTail = "a", PhonemeXSHead = "ts"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ツ"  , PhonemeHead = "ts", PhonemeTail = "u", PhonemeXSHead = "ts"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ッ"  , PhonemeHead = "cl", PhonemeTail = "" , PhonemeXSHead = "sil" , PhonemeXSTail = ""  , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ヂ"  , PhonemeHead = "j" , PhonemeTail = "i", PhonemeXSHead = "dZ"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "チョ", PhonemeHead = "ch", PhonemeTail = "o", PhonemeXSHead = "tS"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "チュ", PhonemeHead = "ch", PhonemeTail = "u", PhonemeXSHead = "tS"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "チャ", PhonemeHead = "ch", PhonemeTail = "a", PhonemeXSHead = "tS"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "チェ", PhonemeHead = "ch", PhonemeTail = "e", PhonemeXSHead = "tS"  , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "チ"  , PhonemeHead = "ch", PhonemeTail = "i", PhonemeXSHead = "tS"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ダ"  , PhonemeHead = "d" , PhonemeTail = "a", PhonemeXSHead = "d"   , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "タ"  , PhonemeHead = "t" , PhonemeTail = "a", PhonemeXSHead = "t"   , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ゾ"  , PhonemeHead = "z" , PhonemeTail = "o", PhonemeXSHead = "dz"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ソ"  , PhonemeHead = "s" , PhonemeTail = "o", PhonemeXSHead = "s"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ゼ"  , PhonemeHead = "z" , PhonemeTail = "e", PhonemeXSHead = "dz"  , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "セ"  , PhonemeHead = "s" , PhonemeTail = "e", PhonemeXSHead = "s"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ズィ", PhonemeHead = "z" , PhonemeTail = "i", PhonemeXSHead = "dz"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ズ"  , PhonemeHead = "z" , PhonemeTail = "u", PhonemeXSHead = "dz"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "スィ", PhonemeHead = "s" , PhonemeTail = "i", PhonemeXSHead = "s"   , PhonemeXSTail = "i" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ス"  , PhonemeHead = "s" , PhonemeTail = "u", PhonemeXSHead = "s"   , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ジョ", PhonemeHead = "j" , PhonemeTail = "o", PhonemeXSHead = "dZ"  , PhonemeXSTail = "o" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ジュ", PhonemeHead = "j" , PhonemeTail = "u", PhonemeXSHead = "dZ"  , PhonemeXSTail = "M" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ジャ", PhonemeHead = "j" , PhonemeTail = "a", PhonemeXSHead = "dZ"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ジェ", PhonemeHead = "j" , PhonemeTail = "e", PhonemeXSHead = "dZ"  , PhonemeXSTail = "e" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ジ"  , PhonemeHead = "j" , PhonemeTail = "i", PhonemeXSHead = "dZ"  , PhonemeXSTail = "i" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "ショ", PhonemeHead = "sh", PhonemeTail = "o", PhonemeXSHead = "S"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "シュ", PhonemeHead = "sh", PhonemeTail = "u", PhonemeXSHead = "S"   , PhonemeXSTail = "M" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "シャ", PhonemeHead = "sh", PhonemeTail = "a", PhonemeXSHead = "S"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "シェ", PhonemeHead = "sh", PhonemeTail = "e", PhonemeXSHead = "S"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "シ"  , PhonemeHead = "sh", PhonemeTail = "i", PhonemeXSHead = "S"   , PhonemeXSTail = "i" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ザ"  , PhonemeHead = "z" , PhonemeTail = "a", PhonemeXSHead = "dz"  , PhonemeXSTail = "a" , PhonemeXSN = "n" },
            new jpcommonMora { moraValue = "サ"  , PhonemeHead = "s" , PhonemeTail = "a", PhonemeXSHead = "s"   , PhonemeXSTail = "a" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ゴ"  , PhonemeHead = "g" , PhonemeTail = "o", PhonemeXSHead = "g"   , PhonemeXSTail = "o" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "コ"  , PhonemeHead = "k" , PhonemeTail = "o", PhonemeXSHead = "k"   , PhonemeXSTail = "o" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "ゲ"  , PhonemeHead = "g" , PhonemeTail = "e", PhonemeXSHead = "g"   , PhonemeXSTail = "e" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "ケ"  , PhonemeHead = "k" , PhonemeTail = "e", PhonemeXSHead = "k"   , PhonemeXSTail = "e" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "ヶ"  , PhonemeHead = "k" , PhonemeTail = "e", PhonemeXSHead = "k"   , PhonemeXSTail = "e" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "グヮ", PhonemeHead = "gw", PhonemeTail = "a", PhonemeXSHead = "g w" , PhonemeXSTail = "a" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "グ"  , PhonemeHead = "g" , PhonemeTail = "u", PhonemeXSHead = "g"   , PhonemeXSTail = "M" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "クヮ", PhonemeHead = "kw", PhonemeTail = "a", PhonemeXSHead = "k w" , PhonemeXSTail = "a" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "ク"  , PhonemeHead = "k" , PhonemeTail = "u", PhonemeXSHead = "k"   , PhonemeXSTail = "M" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "ギョ", PhonemeHead = "gy", PhonemeTail = "o", PhonemeXSHead = "g'"  , PhonemeXSTail = "o" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "ギュ", PhonemeHead = "gy", PhonemeTail = "u", PhonemeXSHead = "g'"  , PhonemeXSTail = "M" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "ギャ", PhonemeHead = "gy", PhonemeTail = "a", PhonemeXSHead = "g'"  , PhonemeXSTail = "a" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "ギェ", PhonemeHead = "gy", PhonemeTail = "e", PhonemeXSHead = "g'"  , PhonemeXSTail = "e" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "ギ"  , PhonemeHead = "g" , PhonemeTail = "i", PhonemeXSHead = "g'"  , PhonemeXSTail = "i" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "キョ", PhonemeHead = "ky", PhonemeTail = "o", PhonemeXSHead = "k'"  , PhonemeXSTail = "o" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "キュ", PhonemeHead = "ky", PhonemeTail = "u", PhonemeXSHead = "k'"  , PhonemeXSTail = "M" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "キャ", PhonemeHead = "ky", PhonemeTail = "a", PhonemeXSHead = "k'"  , PhonemeXSTail = "a" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "キェ", PhonemeHead = "ky", PhonemeTail = "e", PhonemeXSHead = "k'"  , PhonemeXSTail = "e" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "キ"  , PhonemeHead = "k" , PhonemeTail = "i", PhonemeXSHead = "k'"  , PhonemeXSTail = "i" , PhonemeXSN = "N'" },
            new jpcommonMora { moraValue = "ガ"  , PhonemeHead = "g" , PhonemeTail = "a", PhonemeXSHead = "g"   , PhonemeXSTail = "a" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "カ"  , PhonemeHead = "k" , PhonemeTail = "a", PhonemeXSHead = "k"   , PhonemeXSTail = "a" , PhonemeXSN = "N" },
            new jpcommonMora { moraValue = "オ"  , PhonemeHead = "o" , PhonemeTail = "" , PhonemeXSHead = "o"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ォ"  , PhonemeHead = "o" , PhonemeTail = "" , PhonemeXSHead = "o"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "エ"  , PhonemeHead = "e" , PhonemeTail = "" , PhonemeXSHead = "e"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ェ"  , PhonemeHead = "e" , PhonemeTail = "" , PhonemeXSHead = "e"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ウォ", PhonemeHead = "w" , PhonemeTail = "o", PhonemeXSHead = "w"   , PhonemeXSTail = "o" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ウェ", PhonemeHead = "w" , PhonemeTail = "e", PhonemeXSHead = "w"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ウィ", PhonemeHead = "w" , PhonemeTail = "i", PhonemeXSHead = "w"   , PhonemeXSTail = "i" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ウ"  , PhonemeHead = "u" , PhonemeTail = "" , PhonemeXSHead = "M"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ゥ"  , PhonemeHead = "u" , PhonemeTail = "" , PhonemeXSHead = "M"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "イェ", PhonemeHead = "y" , PhonemeTail = "e", PhonemeXSHead = "j"   , PhonemeXSTail = "e" , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "イ"  , PhonemeHead = "i" , PhonemeTail = "" , PhonemeXSHead = "i"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ィ"  , PhonemeHead = "i" , PhonemeTail = "" , PhonemeXSHead = "i"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ア"  , PhonemeHead = "a" , PhonemeTail = "" , PhonemeXSHead = "a"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ァ"  , PhonemeHead = "a" , PhonemeTail = "" , PhonemeXSHead = "a"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ー"  , PhonemeHead = "a" , PhonemeTail = "" , PhonemeXSHead = "-"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ー"  , PhonemeHead = "i" , PhonemeTail = "" , PhonemeXSHead = "-"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ー"  , PhonemeHead = "u" , PhonemeTail = "" , PhonemeXSHead = "-"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ー"  , PhonemeHead = "e" , PhonemeTail = "" , PhonemeXSHead = "-"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = "ー"  , PhonemeHead = "o" , PhonemeTail = "" , PhonemeXSHead = "-"   , PhonemeXSTail = ""  , PhonemeXSN = "N\\" },
            new jpcommonMora { moraValue = ""    , PhonemeHead = ""  , PhonemeTail = "" , PhonemeXSHead = ""    , PhonemeXSTail = ""  , PhonemeXSN = "" }
        };

        //発音記号を求める
        static public string PhonemeXS(string moraValue, string Phoneme)
        {
            //無声化している場合は音符に「Asp」を設定する
            switch (Phoneme)
            {
                case "A":
                case "I":
                case "U":
                case "E":
                case "O":
                    return "Asp";
            };

            //発音記号はモーラと音素をもとに配列検索して求める
            for (int i = 0; i < jpcommonMoraArray.Length; i++)
            {
                if ((moraValue == jpcommonMoraArray[i].moraValue) &
                    (Phoneme   == jpcommonMoraArray[i].PhonemeHead))
                {
                    return jpcommonMoraArray[i].PhonemeXSHead;
                }else
                if ((moraValue == jpcommonMoraArray[i].moraValue) &
                    (Phoneme   == jpcommonMoraArray[i].PhonemeTail))
                {
                    return jpcommonMoraArray[i].PhonemeXSTail;
                }
            };
            return Phoneme;
        }

        //「ん」の発音記号を求める
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

        // vsq3オブジェクトの初期化
        static vsq3 VSQX_Init()
        {
            vsq3 VSQX = new vsq3();

            VSQX.vender = new XmlDocument().CreateCDataSection("Yamaha corporation");
            VSQX.version = new XmlDocument().CreateCDataSection("3.0.0.11");

            VSQX.vVoiceTable = new vVoice[] { new vVoice { } };

            VSQX.vVoiceTable[0].compID = new XmlDocument().CreateCDataSection("BCNFCY43LB2LZCD4");
            VSQX.vVoiceTable[0].vBS = 0;
            VSQX.vVoiceTable[0].vPC = 0;
            VSQX.vVoiceTable[0].vVoiceName = new XmlDocument().CreateCDataSection("MIKU_V4X_Original_EVEC");

            VSQX.vVoiceTable[0].vVoiceParam = new vVoiceParam { };

            VSQX.vVoiceTable[0].vVoiceParam.bre = 0;
            VSQX.vVoiceTable[0].vVoiceParam.bri = 0;
            VSQX.vVoiceTable[0].vVoiceParam.cle = 0;
            VSQX.vVoiceTable[0].vVoiceParam.gen = 0;
            VSQX.vVoiceTable[0].vVoiceParam.ope = 0;

            VSQX.mixer = new mixer { };

            VSQX.mixer.masterUnit = new masterUnit { };

            VSQX.mixer.masterUnit.outDev = 0;
            VSQX.mixer.masterUnit.retLevel = 0;
            VSQX.mixer.masterUnit.vol = 0;

            VSQX.mixer.vsUnit = new vsUnit[] { new vsUnit { } };

            VSQX.mixer.vsUnit[0].vsTrackNo = 0;
            VSQX.mixer.vsUnit[0].inGain = 0;
            VSQX.mixer.vsUnit[0].sendLevel = -898;
            VSQX.mixer.vsUnit[0].sendEnable = 0;
            VSQX.mixer.vsUnit[0].mute = 0;
            VSQX.mixer.vsUnit[0].solo = 0;
            VSQX.mixer.vsUnit[0].pan = 64;
            VSQX.mixer.vsUnit[0].vol = 0;

            VSQX.mixer.seUnit = new seUnit { };

            VSQX.mixer.seUnit.inGain = 0;
            VSQX.mixer.seUnit.sendLevel = -898;
            VSQX.mixer.seUnit.sendEnable = 0;
            VSQX.mixer.seUnit.mute = 0;
            VSQX.mixer.seUnit.solo = 0;
            VSQX.mixer.seUnit.pan = 64;
            VSQX.mixer.seUnit.vol = 0;

            VSQX.mixer.karaokeUnit = new karaokeUnit { };

            VSQX.mixer.karaokeUnit.inGain = 0;
            VSQX.mixer.karaokeUnit.mute = 0;
            VSQX.mixer.karaokeUnit.solo = 0;
            VSQX.mixer.karaokeUnit.vol = -129;

            VSQX.masterTrack = new masterTrack { };

            VSQX.masterTrack.seqName = new XmlDocument().CreateCDataSection("Untitled1");
            VSQX.masterTrack.comment = new XmlDocument().CreateCDataSection("New VSQ File");
            VSQX.masterTrack.resolution = 480;
            VSQX.masterTrack.preMeasure = 1;

            VSQX.masterTrack.timeSig = new timeSig[] { new timeSig { } };

            VSQX.masterTrack.timeSig[0].posMes = 0;
            VSQX.masterTrack.timeSig[0].nume = 4;
            VSQX.masterTrack.timeSig[0].denomi = 4;

            VSQX.masterTrack.tempo = new tempo[] { new tempo { } };

            VSQX.masterTrack.tempo[0].posTick = 0;
            VSQX.masterTrack.tempo[0].bpm = 15000;

            VSQX.vsTrack = new vsTrack[] { new vsTrack { } };

            VSQX.vsTrack[0].vsTrackNo = 0;
            VSQX.vsTrack[0].trackName = new XmlDocument().CreateCDataSection("Track");
            VSQX.vsTrack[0].comment = new XmlDocument().CreateCDataSection("Track");

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

            VSQX.aux[0].auxID = new XmlDocument().CreateCDataSection("AUX_VST_HOST_CHUNK_INFO");
            VSQX.aux[0].content = new XmlDocument().CreateCDataSection("VlNDSwAAAAADAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=");

            return VSQX;

        }

        //音符を編集する
        static note VSQX_note(TIF I01TIF, byte noteNumValue, Double posTickValue, Double durTickValue)
        {
            string PhonemeXSValue;

            //空白は音符の長さを64分音符1個分にする(メインロジックと重複しているが念のため入れておく)
            if (I01TIF.PhonemeCurrent == "sil" | I01TIF.PhonemeCurrent == "pau")
                {
                PhonemeXSValue = "Asp";
                durTickValue = 30;
            }
            else
            {
                PhonemeXSValue = PhonemeXS(I01TIF.MoraCurrent, I01TIF.PhonemeCurrent);
                if (I01TIF.MoraCurrent == "ン")
                {
                    PhonemeXSValue = PhonemeXSN(I01TIF.PhonemeNext, I01TIF.PhonemeAfterNext);
                }
            };

            var nt = new note
            {
                posTick  = (int)posTickValue,
                durTick  = (int)durTickValue,
                noteNum  = noteNumValue,
                velocity = 64,
                lyric    = new XmlDocument().CreateCDataSection(I01TIF.PhonemeCurrent),

                phnms = new typePhonemes
                {
                    //歌詞がトレース情報の音素情報であり発音記号に変換できないため、プロテクトをかけて発音記号が崩れないようにする。
                    @lock = 1,
                    lockSpecified = true,
                    Value         = PhonemeXSValue
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

        //メインプログラム
        static void Main(string[] args)
        {

            // 本来はファイルのパスはパラメータで取得するか、実行ファイルのパスに設定するものだが暫定的にソース内直書き
            string sTIFPass  = @"C:\test\open-jtalk\t.txt";    //トレース情報
            string sTIFXPass = @"C:\test\open-jtalk\tA.xml";   //出力VSQXファイル
            string sVSQXPass = @"C:\test\open-jtalk\tA.vsqx";  //トレース情報のリストをシリアライズしたもの

            System.IO.StreamReader srTIF = new System.IO.StreamReader(sTIFPass, Encoding.GetEncoding("SHIFT_JIS"));

            // vsq3オブジェクトの初期設定
            var VSQX = VSQX_Init();

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

            TIF I01TIF;
            string I01_Line = srTIF.ReadLine();
            while (srTIF.EndOfStream == false)
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
                int MoraLocDownStart  = 0;
                int MoraNoteIdxDownStart = 0;
                String MoraEnd     = "";
                int MoraLocEnd     = 0;
                int MoraNoteIdxEnd = 0;

                //音符位置を64分音符の個数に変換する。BPM150なので64分音符1個は25ms。
                posTickValue = I01TIF.PhonemeFrom / 10000 / 25.00;
                //音符位置をtick値に変換する。64分音符1個は30tick。
                posTickValue = Math.Round(posTickValue) * 30;

                //呼気段落末までの繰り返し
                while (I01TIF.BreathPosInUtteranceForward != "xx")
                {

                    //音符長さを64分音符の個数に変換する。BPM150なので64分音符1個は25ms。
                    durTickValue = (I01TIF.PhonemeTo - I01TIF.PhonemeFrom) / 10000 / 25.00;
                    //音符長さをtick値に変換する。64分音符1個は30tick。
                    durTickValue = Math.Round(durTickValue) * 30;

                    //音符を配列に追加
                    noteArray[noteIdx] = VSQX_note(I01TIF, 62, posTickValue, durTickValue);
                    TIFArray[noteIdx]  = I01TIF;

                    //１モーラ目～４モーラ目までの開始位置の音符の位置情報を取得する
                    if (Mora1 == "")
                    {
                        if (I01TIF.MoraPosForward == "1")
                        {
                            Mora1        = I01TIF.MoraCurrent;
                            MoraLoc1     = (int)posTickValue;
                            MoraNoteIdx1 = noteIdx;
                        }
                    }

                    if (Mora2 == "")
                    {
                        if (I01TIF.MoraPosForward == "2")
                        {
                            Mora2        = I01TIF.MoraCurrent;
                            MoraLoc2     = (int)posTickValue;
                            MoraNoteIdx2 = noteIdx;
                        }
                    }

                    if (Mora3 == "")
                    {
                        if (I01TIF.MoraPosForward == "3")
                        {
                            Mora3        = I01TIF.MoraCurrent;
                            MoraLoc3     = (int)posTickValue;
                            MoraNoteIdx3 = noteIdx;
                        }
                    }

                    if (Mora4 == "")
                    {
                        if (I01TIF.MoraPosForward == "4")
                        {
                            Mora4        = I01TIF.MoraCurrent;
                            MoraLoc4     = (int)posTickValue;
                            MoraNoteIdx4 = noteIdx;
                        }
                    }

                    if (MoraDownStart == "")
                    {
                        if (I01TIF.MoraDiffAccent == "1")
                        {
                            MoraDownStart        = I01TIF.MoraCurrent;
                            MoraLocDownStart     = (int)posTickValue + (int)durTickValue ;
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

                //呼気段落内に音素がある場合は(「音素がない」のは先頭空白のみのはず）
                if (noteIdxInUtterance != 0)
                {
                    //句頭上昇位置を求める。基本は２モーラ目直後の音符
                    MoraTop          = Mora2;
                    MoraLocTop       = MoraLoc3;
                    MoraNoteIdxTop   = MoraNoteIdx3;
                    MoraLocStart     = MoraLoc2;
                    MoraNoteIdxStart = MoraNoteIdx2;

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
                    if (MoraNoteIdxDownStart == MoraNoteIdx2)
                    {
                        MoraTop          = Mora1;
                        MoraLocTop       = MoraLoc2;
                        MoraNoteIdxTop   = MoraNoteIdx2;
                        MoraLocStart     = MoraLoc1;
                        MoraNoteIdxStart = MoraNoteIdx1;
                    }

                    //句頭上昇部分の音階を設定する
                    noteArray[MoraNoteIdxTop].noteNum     = 70;
                    noteArray[MoraNoteIdxTop - 1].noteNum = 67;

                    //音階頂点位置から呼気段落末(音の高さは自然下降のみとする)までの傾きを求める
                    double SlopeNatural = (68.00 - 70.00) / (MoraLocEnd - MoraLocTop);

                    //一旦、音の高さは自然下降のみとして、音階頂点位置から呼気段落末手前までの音階を設定する
                    for (int i = MoraNoteIdxTop + 1; i <= MoraNoteIdxEnd; i++)
                    {
                        double x1 = (noteArray[i].posTick - noteArray[MoraNoteIdxTop].posTick);
                        x1 = x1 * SlopeNatural;
                        x1 = x1 + 70.00;
                        noteArray[i].noteNum = (byte)Math.Round(x1);
                    };

                    //アクセント核がある場合は
                    if (MoraDownStart != "")
                    {
                        //下降開始位置から呼気段落末までの傾きを求める
                        double SlopeEnd = (60.00 - noteArray[MoraNoteIdxDownStart].noteNum) / (MoraLocEnd - MoraLocDownStart);
                        //下降開始位置から呼気段落末までの音階を設定する
                        for (int i = MoraNoteIdxDownStart + 2; i <= MoraNoteIdxEnd; i++)
                        {
                            double x2 = (noteArray[i].posTick - noteArray[MoraNoteIdxDownStart].posTick);
                            x2 = x2 * SlopeEnd;
                            x2 = x2 + noteArray[MoraNoteIdxDownStart].noteNum;
                            noteArray[i].noteNum = (byte)Math.Round(x2);
                            //下降中はアクセント核のモーラが来るまで場合は音階を維持する
                            if (TIFArray[i].MoraDiffAccent.CompareTo("1") <= 0)
                            {
                                noteArray[i].noteNum = noteArray[i - 1].noteNum;
                            }
                        };
                    };

                    //呼気段落末直後の空白音符の音階を設定する。疑問文は音階を上げる
                    if (I01TIF.IsInterrogativePrevAcc == "0")
                    {
                        if(MoraDownStart == "")
                        {
                            noteNumValue = 68;
                        }
                        else
                        {
                            noteNumValue = 60;
                        }
                    }
                    else
                    {
                        noteNumValue = 64;
                    }

                    //呼気段落末直後の空白音符の長さは64分音符1個分
                    durTickValue = 30;

                    //呼気段落末直後の空白音符を追加する
                    noteArray[noteIdx] = VSQX_note(I01TIF, noteNumValue, posTickValue, durTickValue);
                    noteIdx++;
                    T01TIF.Add(I01TIF);

                 }
                 I01_Line = srTIF.ReadLine();
            }
            srTIF.Close();

            // 音階を設定した音符情報をvsq3オブジェクトに設定する
            ((musicalPart)(VSQX.vsTrack[0].Items[0])).note = noteArray;

            // パート長さは最後の音符位置から一拍あと
            ((musicalPart)(VSQX.vsTrack[0].Items[0])).playTime = (int)posTickValue + 30 + 480;
            
            // トレース情報のリストをシリアライズする
            var xmlSerializer1 = new XmlSerializer(typeof(List<TIF>));
            using (var streamWriter = new StreamWriter(sTIFXPass, false, Encoding.UTF8))
            {
                xmlSerializer1.Serialize(streamWriter, T01TIF);
                streamWriter.Flush();
            }

            // vsq3オブジェクトをシリアライズする
            var xmlSerializer2 = new XmlSerializer(typeof(vsq3));
            using (var streamWriter = new StreamWriter(sVSQXPass, false, Encoding.UTF8))
            {
                xmlSerializer2.Serialize(streamWriter, VSQX);
                streamWriter.Flush();
            }

        }
   }
}
