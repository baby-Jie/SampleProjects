using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RichTextBoxSample
{
    public partial class MainWindow : Window
    {
        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region  Fields
        #endregion Fields

        #region Properties

        #endregion Properties

        #region Methods

        #region Private Methods

        /// <summary>
        /// 将RichTextBox中的文本 转换成图片并设置成背景
        /// </summary>
        private void SaveRichTextAsImage(FlowDocument flowDocument)
        {
            var height = RichTextEditor.ActualHeight - 2;

            var width = RichTextEditor.ActualWidth - 2;

            int bold = flowDocument.FontWeight == FontWeights.Bold ? 1 : 0;

            var italic = flowDocument.FontStyle == FontStyles.Italic ? 2 : 0;

            int underline = 0;

            var fontStyle = (System.Drawing.FontStyle)(bold + italic + underline);

            var content = GetTextEditorContent(flowDocument);

            using (Bitmap bitmap = new Bitmap((int)width, (int)height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {

                    Font font = new System.Drawing.Font("宋体", (float)flowDocument.FontSize, fontStyle, GraphicsUnit.Pixel);

                    RectangleF rectangleF = new RectangleF(4.1f, 12, (float)width, (float)height);

                    GraphicsContainer gc = g.BeginContainer();

                    try
                    {
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.Default; // PixelOffsetMode.HighQuality 在插入带有文字的图片变模糊
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        if (fontStyle == (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))
                        {
                            // 如果是斜体加上粗体 那么 选择 SingleBitPerPixelGridFit 模式
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                        }
                        else
                        {
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                        }
                        g.CompositingMode = CompositingMode.SourceOver;
                        g.CompositingQuality = CompositingQuality.HighQuality;

                        g.DrawString(content, font, System.Drawing.Brushes.Red, rectangleF,
                            StringFormat.GenericTypographic);

                    }
                    catch (Exception e)
                    {

                    }
                    finally
                    {
                        g.EndContainer(gc);
                    }
                }

                #region 保存为图片和生成背景图片

                var bitmapSource = Bitmap2BitmapSource(bitmap);
                var imageBrush = new ImageBrush(bitmapSource);
                imageBrush.Stretch = Stretch.None;
                RichTextEditor.Background = imageBrush;
                bitmap.Save("test.png");

                #endregion 保存为图片和生成背景图片
            }
        }

        /// <summary>
        /// 获取文本内容 不带\r\n 
        /// </summary>
        /// <returns></returns>
        private string GetTextEditorContent(FlowDocument flowDocument)
        {
            string content = string.Empty;

            try
            {
                var textrange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                content = textrange.Text;

                int index = -1;

                for (var i = content.Length - 1; i >= 0; i--)
                {
                    if (content[i] != '\r' && content[i] != '\n')
                    {
                        index = i;
                        break;
                    }
                }

                if (index >= 0 && index != content.Length - 1)
                {
                    content = content.Substring(0, index + 1);
                }
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
            }

            return content;
        }

        /// <summary>
        /// 获取文本内容 末尾带\r\n
        /// </summary>
        /// <returns></returns>
        private string GetTextEditorContent()
        {
            string content = string.Empty;

            try
            {
                var textrange = new TextRange(EditorDocument.ContentStart, EditorDocument.ContentEnd);
                content = textrange.Text;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
            }

            return content;
        }

        /// <summary>
        /// 将System.Drawing.Bitmap 转化为 BitmapSource
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapSource Bitmap2BitmapSource(System.Drawing.Bitmap bitmap)
        {
            IntPtr bmpPt = IntPtr.Zero;
            BitmapSource bitmapSource = null;
            try
            {
                //bitmap = new System.Drawing.Bitmap(myImage);
                bmpPt = bitmap.GetHbitmap();
                bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bmpPt,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (bitmapSource != null)
                    bitmapSource.Freeze();
                if (bmpPt != IntPtr.Zero)
                {
                    DeleteObject(bmpPt);
                }
            }
            return bitmapSource;
        }

        #endregion Private Methods	

        #region Native Methods

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);

        #endregion Native Methods

        #endregion Methods

        #region EventHandlers

        /// <summary>
        /// 设置粗体 或者取消粗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetBoldBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (EditorDocument.FontWeight == FontWeights.Bold)
            {
                EditorDocument.FontWeight = FontWeights.Normal;
            }
            else
            {
                EditorDocument.FontWeight = FontWeights.Bold;
            }
        }

        /// <summary>
        /// 设置斜体 或者取消设置斜体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetItalicBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (EditorDocument.FontStyle == FontStyles.Italic)
            {
                EditorDocument.FontStyle = FontStyles.Normal;
            }
            else
            {
                EditorDocument.FontStyle = FontStyles.Italic;
            }
        }

        /// <summary>
        /// 设置下划线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetUnderLineBtn_OnClick(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// 将编辑的内容保存到图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveImageBtn_OnClick(object sender, RoutedEventArgs e)
        {
            SaveRichTextAsImage(EditorDocument);
            //SaveRichTextAsImage(RichTextEditor, EditorDocument);
        }

        #endregion EventHandlers
    }
}
