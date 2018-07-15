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

        static vsq3 VSQX_Init()
        {
            // VSQXのクラス初期化
            vsq3 VSQX = new vsq3();

            VSQX.vender  = new XmlDocument().CreateCDataSection("Yamaha corporation");
            VSQX.version = new XmlDocument().CreateCDataSection("3.0.0.11");

            VSQX.vVoiceTable = new vVoice[] { new vVoice { } };

            VSQX.vVoiceTable[0].compID     = new XmlDocument().CreateCDataSection("BCNFCY43LB2LZCD4");
            VSQX.vVoiceTable[0].vBS        = 0;
            VSQX.vVoiceTable[0].vPC        = 0;
            VSQX.vVoiceTable[0].vVoiceName = new XmlDocument().CreateCDataSection("MIKU_V4X_Original_EVEC");

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
            VSQX.masterTrack.preMeasure = 1;

            VSQX.masterTrack.timeSig = new timeSig[] { new timeSig { } };

            VSQX.masterTrack.timeSig[0].posMes = 0;
            VSQX.masterTrack.timeSig[0].nume   = 4;
            VSQX.masterTrack.timeSig[0].denomi = 4;

            VSQX.masterTrack.tempo = new tempo[] { new tempo { } };

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

                    note = new note[]
                    {
                        new note
                        {
                            posTick  = 0,
                            durTick  = 480,
                            noteNum  = 60,
                            velocity = 64,
                            lyric    = new XmlDocument().CreateCDataSection("ら"),

                            phnms    = new typePhonemes
                            {
                                @lock = 1,
                                lockSpecified = true,
                                Value = "4 a"
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
                        }
                    }

                }
            };

            VSQX.seTrack = new wavPart[] { };

            VSQX.karaokeTrack = new wavPart[] { };

            VSQX.aux = new aux[] { new aux { } };

            VSQX.aux[0].auxID   = new XmlDocument().CreateCDataSection("AUX_VST_HOST_CHUNK_INFO");
            VSQX.aux[0].content = new XmlDocument().CreateCDataSection("VlNDSwAAAAADAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=");

            return VSQX;

        }

        static void Main(string[] args)
        {
            string sTIFPass  = @"C:\test\open-jtalk\t.txt";
            string sTIFXPass = @"C:\test\open-jtalk\tA.xml";
            string sVSQXPass = @"C:\test\open-jtalk\tA.vsqx";

            System.IO.StreamReader srTIF = new System.IO.StreamReader(sTIFPass, Encoding.GetEncoding("SHIFT_JIS"));

            // トレース情報をリストに格納する
            var T01TIF = new List<TIF>();
            while (srTIF.EndOfStream == false)
            {
                string I01_Line = srTIF.ReadLine();
                TIF I01TIF = new TIF(I01_Line);
                T01TIF.Add(I01TIF);
            }
            srTIF.Close();

            var VSQX = VSQX_Init();

            // トレース情報をシリアライズする
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
