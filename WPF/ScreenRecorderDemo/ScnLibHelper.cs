using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZDSoft;

namespace ScreenRecorderDemo
{
    public class ScnLibHelper
    {

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static bool InitialRecord()
        {
            return ZDSoft.SDK.ScnLib_Initialize();
        }

        public static void CommonInitialRecord()
        {
            InitialRecord();
            EnableAudio(true);
            EnableAudio(false);
        }

        /// <summary>
        /// 开始录制
        /// </summary>
        /// <returns></returns>
        public static bool StartRecord()
        {
            return ZDSoft.SDK.ScnLib_StartRecording();
        }

        /// <summary>
        /// 暂停录制
        /// </summary>
        /// <returns></returns>
        public static bool PauseRecord()
        {
            return ZDSoft.SDK.ScnLib_PauseRecording();
        }

        /// <summary>
        /// 停止录制
        /// </summary>
        public static void StopRecord()
        {
             ZDSoft.SDK.ScnLib_StopRecording();
        }

        /// <summary>
        /// 开启音频录制
        /// </summary>
        /// <param name="playback"></param>
        public static void EnableAudio(bool playback)
        {
            ZDSoft.SDK.ScnLib_RecordAudioSource(playback, true);
        }

        /// <summary>
        /// 关闭音频录制
        /// </summary>
        /// <param name="playback"></param>
        public static void DisableAudio(bool playback)
        {
            ZDSoft.SDK.ScnLib_RecordAudioSource(playback, false);
        }

        /// <summary>
        /// 录制音频
        /// </summary>
        public static void RecordAudio()
        {
            // 先清空 video path
            ZDSoft.SDK.ScnLib_SetVideoPathW(string.Empty);

            string audioPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            string audioFileName = Path.Combine(audioPath, "test.mp3");
            ZDSoft.SDK.ScnLib_SetAudioPathW(audioFileName);
        }

        public static string GetAudioPath()
        {
            StringBuilder sb = new StringBuilder(1025);
            ZDSoft.SDK.ScnLib_GetAudioPathW(sb, true);
            return sb.ToString();
        }

        public static void SetLogText(string text, Font font, Color color)
        {
            LOGFONT logFont = new LOGFONT();
            font.ToLogFont(logFont);

            uint uintColor = 0;
            uintColor = uintColor | color.B;
            uintColor = uintColor << 8;

            uintColor = uintColor | color.G;
            uintColor = uintColor << 8;

            uintColor = uintColor | color.R;

            ZDSoft.SDK.ScnLib_SetLogoTextW(text, logFont, uintColor, false);
        }
    }
}
