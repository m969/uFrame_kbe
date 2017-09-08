// Copyright (c) 2017 Doozy Entertainment / Marlink Trading SRL and Ez Entertainment / Ez Entertainment SRL. All Rights Reserved.
// This code is a collaboration between Doozy Entertainment and Ez Entertainment and is not to be used in any other assets other then the ones created by their respective companies.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

namespace QuickEditor
{
    public partial class QResources
    {
        public static QGeneratedTexture TransparentBackground = new QGeneratedTexture(new QColor(0, 0, 0, 0));

        public static QGeneratedTexture WhiteBackground = new QGeneratedTexture(new QColor(QuickEngine.QColors.WhiteDark.Color, QuickEngine.QColors.WhiteLight.Color));

        public static QGeneratedTexture HelpBackground = new QGeneratedTexture(new QColor(QuickEngine.QColors.WhiteDark.Color, QuickEngine.QColors.WhiteLight.Color), new QColor(QColors.Help.Dark, QColors.Help.Light));
        public static QGeneratedTexture InfoBackground = new QGeneratedTexture(new QColor(QuickEngine.QColors.WhiteDark.Color, QuickEngine.QColors.WhiteLight.Color), new QColor(QColors.Info.Dark, QColors.Info.Light));
        public static QGeneratedTexture WarningBackground = new QGeneratedTexture(new QColor(QuickEngine.QColors.WhiteDark.Color, QuickEngine.QColors.WhiteLight.Color), new QColor(QColors.Warning.Dark, QColors.Warning.Light));
        public static QGeneratedTexture ErrorBackground = new QGeneratedTexture(new QColor(QuickEngine.QColors.WhiteDark.Color, QuickEngine.QColors.WhiteLight.Color), new QColor(QColors.Error.Dark, QColors.Error.Light));
    }
}
