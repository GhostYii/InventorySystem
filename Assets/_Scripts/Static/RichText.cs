public static class RichText
{
    public static class HexColor
    {
        /// <summary>
        /// 红
        /// </summary>
        public static readonly string Red = "<color=#ff0000ff>";
        /// <summary>
        /// 浅绿
        /// </summary>
        public static readonly string Aqua = "<color=#00ffffff>";
        /// <summary>
        /// 黑色
        /// </summary>
        public static readonly string Black = "<color=#000000ff>";
        /// <summary>
        /// 蓝
        /// </summary>
        public static readonly string Blue = "<color=#0000ffff>";
        /// <summary>
        /// 棕
        /// </summary>
        public static readonly string Brown = "<color=#a52a2aff>";
        /// <summary>
        /// 青
        /// </summary>
        public static readonly string Cyan = Aqua;
        /// <summary>
        /// 深蓝
        /// </summary>
        public static readonly string DarkBlue = "<color=#0000a0ff>";
        /// <summary>
        /// 紫红
        /// </summary>
        public static readonly string Fuchsia = "<color=#ff00ffff>";
        /// <summary>
        /// 绿
        /// </summary>
        public static readonly string Green = "<color=#008000ff>";
        /// <summary>
        /// 灰
        /// </summary>
        public static readonly string Grey = "<color=#808080ff>";
        /// <summary>
        /// 浅蓝
        /// </summary>
        public static readonly string LightBlue = "<color=#add8e6ff>";
        /// <summary>
        ///  绿黄
        /// </summary>
        public static readonly string Lime = "<color=#00ff00ff>";
        /// <summary>
        /// 黄
        /// </summary>
        public static readonly string Yellow = "<color=#ffff00ff>";
        /// <summary>
        /// 浅粉
        /// </summary>
        public static readonly string LightPink = "<color=#ffb6c1ff>";
        /// <summary>
        /// 紫罗兰
        /// </summary>
        public static readonly string Violet = "<color=#ee82eeff>";
        /// <summary>
        /// 紫
        /// </summary>
        public static readonly string Purple = "<color=#800080ff>";
        /// <summary>
        /// 粉红
        /// </summary>
        public static readonly string Pink = "<color=#ffc0cbff>";
        /// <summary>
        /// 蔷薇
        /// </summary>
        public static readonly string Rosa = "<color=#ff5656ff>";
        /// <summary>
        /// 天空蓝
        /// </summary>
        public static readonly string SkyBlue = "<color=#87ceebff>";
        /// <summary>
        /// 薄荷奶油
        /// </summary>
        public static readonly string MintCream = "<color=#00ff7fff>";
        /// <summary>
        /// 蜂蜜
        /// </summary>
        public static readonly string Honeydew = "<color=#f0fff0ff>";
        /// <summary>
        /// 洋红
        /// </summary>
        public static readonly string Magenta = Fuchsia;
        /// <summary>
        /// 米色
        /// </summary>
        public static readonly string Beige = "<color=#6b8e23ff>";
        /// <summary>
        /// 金
        /// </summary>
        public static readonly string Gold = "<color=#ffd700ff>";
        /// <summary>
        /// 橙
        /// </summary>
        public static readonly string Orange = "<color=#ffa500ff>";
        /// <summary>
        /// 巧克力色
        /// </summary>
        public static readonly string Chocolate = "<color=#d2691eff>";
        /// <summary>
        /// 雪
        /// </summary>
        public static readonly string Snow = "<color=#fffafaff>";
        /// <summary>
        /// 白
        /// </summary>
        public static readonly string White = "<color=#ffffffff>";

        public static readonly string EndColor = "</color>";

        public static string GetColorText(string txt, string hexColor)
        {
            return string.Format("{0}{1}{2}", hexColor, txt, EndColor);
        }
    }

    public static class Format
    {
        /// <summary>
        /// 粗体
        /// </summary>
        public static readonly string Bold = "<b>";
        public static readonly string EndBold = "</b>";
        /// <summary>
        /// 斜体
        /// </summary>
        public static readonly string Italic = "<i>";
        public static readonly string EndItatic = "</i>";
        //public static string Size = "<size=>";
        //public static string EndSize = "</size>";
        /// <summary>
        /// 斜粗
        /// </summary>
        public static readonly string BoldAndItatic = "<b><i>";
        public static readonly string EndBoldAndItatic = "</i></b>";

        public static string GetFormatText(string txt,string format)
        {
            switch (format)
            {
                case "<b>":
                    return string.Format("{0}{1}{2}", format, txt, EndBold);
                case "<i>":
                    return string.Format("{0}{1}{2}", format, txt, EndItatic);
                case "<b><i>":
                    return string.Format("{0}{1}{2}", format, txt, EndBoldAndItatic);
                default:
                    return string.Empty;
            }
        }
    }

}
