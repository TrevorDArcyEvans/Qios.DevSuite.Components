// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.NativeMethods
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Qios.DevSuite.Components
{
  internal class NativeMethods
  {
    public const int LAYOUT_BITMAPORIENTATIONPRESERVED = 8;
    public const int LAYOUT_RTL = 1;
    public const int HWND_TOP = 0;
    public const int HWND_BOTTOM = 1;
    public const int HWND_TOPMOST = -1;
    public const int HWND_NOTOPMOST = -2;
    public const int HWND_MESSAGE = -3;
    public const int SBM_SETSCROLLINFO = 233;
    public const int SBM_GETSCROLLINFO = 234;
    public const int STATE_SYSTEM_INVISIBLE = 32768;
    public const uint OBJID_CLIENT = 4294967292;
    public const uint OBJID_VSCROLL = 4294967291;
    public const uint OBJID_HSCROLL = 4294967290;
    public const int WM_USER = 1024;
    public const int WM_SYSCOLORCHANGE = 21;
    public const int WM_SHOWWINDOW = 24;
    public const int WM_ERASEBKGND = 20;
    public const int WM_PAINT = 15;
    public const int WM_SETICON = 128;
    public const int WM_SYSCOMMAND = 274;
    public const int WM_HSCROLL = 276;
    public const int WM_VSCROLL = 277;
    public const int WM_GETMINMAXINFO = 36;
    public const int WM_LBUTTONDOWN = 513;
    public const int WM_RBUTTONDOWN = 516;
    public const int WM_MBUTTONDOWN = 519;
    public const int WM_XBUTTONDOWN = 523;
    public const int WM_LBUTTONDBLCLK = 515;
    public const int WM_RBUTTONDBLCLK = 518;
    public const int WM_MBUTTONDBLCLK = 521;
    public const int WM_XBUTTONDBLCLK = 525;
    public const int WM_LBUTTONUP = 514;
    public const int WM_RBUTTONUP = 517;
    public const int WM_MBUTTONUP = 520;
    public const int WM_XBUTTONUP = 524;
    public const int WM_SIZING = 532;
    public const int WM_CAPTURECHANGED = 533;
    public const int WM_CONTEXTMENU = 123;
    public const int WM_NCCALCSIZE = 131;
    public const int WM_NCHITTEST = 132;
    public const int WM_NCPAINT = 133;
    public const int WM_MOUSEWHEEL = 522;
    public const int WM_MOUSEMOVE = 512;
    public const int WM_NCALLMOUSEBUTTONACTIONS = 160;
    public const int WM_NCMOUSEMOVE = 160;
    public const int WM_NCLBUTTONDOWN = 161;
    public const int WM_NCLBUTTONUP = 162;
    public const int WM_NCLBUTTONDBLCLK = 163;
    public const int WM_NCRBUTTONDOWN = 164;
    public const int WM_NCRBUTTONUP = 165;
    public const int WM_NCRBUTTONDBLCLK = 166;
    public const int WM_NCMBUTTONDOWN = 167;
    public const int WM_NCMBUTTONUP = 168;
    public const int WM_NCMBUTTONDBLCLK = 169;
    public const int WM_NCXBUTTONDOWN = 171;
    public const int WM_NCXBUTTONUP = 172;
    public const int WM_NCXBUTTONDBLCLK = 173;
    public const int WM_COMMAND = 273;
    public const int WM_CTLCOLOREDIT = 307;
    public const int WM_CTLCOLORSTATIC = 312;
    public const int WS_EX_LAYOUTRTL = 4194304;
    public const int WS_EX_NOINHERITLAYOUT = 1048576;
    public const int WS_EX_RIGHT = 4096;
    public const int WS_EX_LEFT = 0;
    public const int WS_EX_RTLREADING = 8192;
    public const int WS_EX_LTRREADING = 0;
    public const int WS_EX_LEFTSCROLLBAR = 16384;
    public const int WS_EX_RIGHTSCROLLBAR = 0;
    public const int PROCESS_ALL_ACCESS = 2035711;
    public const int MEM_COMMIT = 4096;
    public const int MEM_RELEASE = 32768;
    public const int PAGE_READWRITE = 4;
    public const int TB_GETITEMRECT = 1053;
    public const int TB_BUTTONCOUNT = 1048;
    public const int TB_DELETEBUTTON = 1046;
    public const int TB_GETBUTTON = 1047;
    public const int TB_GETBUTTONINFOW = 1087;
    public const int PM_NOREMOVE = 0;
    public const int PM_REMOVE = 1;
    public const int PM_NOYIELD = 2;
    public const int WM_TIMER = 275;
    public const int WM_NCMOUSEHOVER = 672;
    public const int WM_NCMOUSELEAVE = 674;
    public const uint TME_HOVER = 1;
    public const uint TME_LEAVE = 2;
    public const uint TME_NONCLIENT = 16;
    public const uint HOVER_DEFAULT = 4294967295;
    public const int WM_ACTIVATE = 6;
    public const int WM_ACTIVATEAPP = 28;
    public const int WM_MOUSEACTIVATE = 33;
    public const int WM_CHILDACTIVATE = 34;
    public const int WM_CREATE = 1;
    public const int WM_DESTROY = 2;
    public const int WM_SETFOCUS = 7;
    public const int WM_KILLFOCUS = 8;
    public const int WM_CLOSE = 16;
    public const int WM_QUERYENDSESSION = 17;
    public const int WM_ENDSESSION = 22;
    public const int WM_SETCURSOR = 32;
    public const int WM_KEYFIRST = 256;
    public const int WM_KEYDOWN = 256;
    public const int WM_KEYUP = 257;
    public const int WM_CHAR = 258;
    public const int WM_DEADCHAR = 259;
    public const int WM_SYSKEYDOWN = 260;
    public const int WM_SYSKEYUP = 261;
    public const int WM_SYSCHAR = 262;
    public const int WM_PRINT = 791;
    public const int WM_PRINTCLIENT = 792;
    public const int WM_MDISETMENU = 560;
    public const int WM_ENTERSIZEMOVE = 561;
    public const int WM_EXITSIZEMOVE = 562;
    public const int WM_MDIREFRESHMENU = 564;
    public const int WM_WINDOWPOSCHANGING = 70;
    public const int WM_WINDOWPOSCHANGED = 71;
    public const int WM_SETTEXT = 12;
    public const int WM_NCACTIVATE = 134;
    public const int WM_SIZE = 5;
    public const int SIZE_RESTORED = 0;
    public const int SIZE_MINIMIZED = 1;
    public const int SIZE_MAXIMIZED = 2;
    public const int SIZE_MAXSHOW = 3;
    public const int SIZE_MAXHIDE = 4;
    public const int WA_INACTIVE = 0;
    public const int WA_ACTIVE = 1;
    public const int WA_CLICKACTIVE = 2;
    public const int MA_ACTIVATE = 1;
    public const int MA_ACTIVATEANDEAT = 2;
    public const int MA_NOACTIVATE = 3;
    public const int MA_NOACTIVATEANDEAT = 4;
    public const int WVR_ALIGNTOP = 16;
    public const int WVR_ALIGNLEFT = 32;
    public const int WVR_ALIGNBOTTOM = 64;
    public const int WVR_ALIGNRIGHT = 128;
    public const int WVR_HREDRAW = 256;
    public const int WVR_VREDRAW = 512;
    public const int WVR_REDRAW = 768;
    public const int WVR_VALIDRECTS = 1024;
    public const int DCX_WINDOW = 1;
    public const int DCX_EXCLUDERGN = 64;
    public const int DCX_INTERSECTRGN = 128;
    public const int ES_LEFT = 0;
    public const int ES_CENTER = 1;
    public const int ES_RIGHT = 2;
    public const int ES_MULTILINE = 4;
    public const int ES_UPPERCASE = 8;
    public const int ES_LOWERCASE = 16;
    public const int ES_PASSWORD = 32;
    public const int ES_AUTOVSCROLL = 64;
    public const int ES_AUTOHSCROLL = 128;
    public const int ES_NOHIDESEL = 256;
    public const int ES_OEMCONVERT = 1024;
    public const int ES_READONLY = 2048;
    public const int ES_WANTRETURN = 4096;
    public const int ES_NUMBER = 8192;
    public const int EM_GETSEL = 176;
    public const int EM_SETSEL = 177;
    public const int EM_GETRECT = 178;
    public const int EM_SETRECT = 179;
    public const int EM_SETRECTNP = 180;
    public const int EM_SCROLL = 181;
    public const int EM_LINESCROLL = 182;
    public const int EM_SCROLLCARET = 183;
    public const int EM_GETMODIFY = 184;
    public const int EM_SETMODIFY = 185;
    public const int EM_GETLINECOUNT = 186;
    public const int EM_LINEINDEX = 187;
    public const int EM_SETHANDLE = 188;
    public const int EM_GETHANDLE = 189;
    public const int EM_GETTHUMB = 190;
    public const int EM_LINELENGTH = 193;
    public const int EM_REPLACESEL = 194;
    public const int EM_GETLINE = 196;
    public const int EM_LIMITTEXT = 197;
    public const int EM_CANUNDO = 198;
    public const int EM_UNDO = 199;
    public const int EM_FMTLINES = 200;
    public const int EM_LINEFROMCHAR = 201;
    public const int EM_SETTABSTOPS = 203;
    public const int EM_SETPASSWORDCHAR = 204;
    public const int EM_EMPTYUNDOBUFFER = 205;
    public const int EM_GETFIRSTVISIBLELINE = 206;
    public const int EM_SETREADONLY = 207;
    public const int EM_SETWORDBREAKPROC = 208;
    public const int EM_GETWORDBREAKPROC = 209;
    public const int EM_GETPASSWORDCHAR = 210;
    public const int EM_SETMARGINS = 211;
    public const int EM_GETMARGINS = 212;
    public const int EM_SETLIMITTEXT = 197;
    public const int EM_GETLIMITTEXT = 213;
    public const int EM_POSFROMCHAR = 214;
    public const int EM_CHARFROMPOS = 215;
    public const int PRF_CHECKVISIBLE = 1;
    public const int PRF_NONCLIENT = 2;
    public const int PRF_CLIENT = 4;
    public const int PRF_ERASEBKGND = 8;
    public const int PRF_CHILDREN = 16;
    public const int PRF_OWNED = 32;
    public const int WM_THEMECHANGED = 794;
    public const int DTBG_MIRRORDC = 32;
    public const int TMT_SIZINGMARGINS = 3601;
    public const int TMT_CONTENTMARGINS = 3602;
    public const int TMT_CAPTIONMARGINS = 3603;
    public const int TMT_BORDERSIZE = 2403;
    public const int WMSZ_BOTTOMRIGHT = 8;
    public const int MK_MBUTTON = 16;
    public const int SC_MOVE = 61456;
    public const int SC_RESTORE = 61728;
    public const int SC_MINIMIZE = 61472;
    public const int SC_MAXIMIZE = 61488;
    public const int SC_HOTKEY = 61776;
    public const int SC_MOUSEMENU = 61584;
    public const int SC_KEYMENU = 61696;
    public const int WM_MDIMAXIMIZE = 549;
    public const int WM_MDIRESTORE = 547;
    public const int WM_MDIGETACTIVE = 553;
    public const int SRCCOPY = 13369376;
    public const int SRCPAINT = 15597702;
    public const int SRCAND = 8913094;
    public const int SRCINVERT = 6684742;
    public const int SRCERASE = 4457256;
    public const int NOTSRCCOPY = 3342344;
    public const int NOTSRCERASE = 1114278;
    public const int MERGECOPY = 12583114;
    public const int MERGEPAINT = 12255782;
    public const int PATCOPY = 15728673;
    public const int PATPAINT = 16452105;
    public const int PATINVERT = 5898313;
    public const int DSTINVERT = 5570569;
    public const int BLACKNESS = 66;
    public const int WHITENESS = 16711778;
    public const int SW_HIDE = 0;
    public const int SW_SHOWNORMAL = 1;
    public const int SW_NORMAL = 1;
    public const int SW_SHOWMINIMIZED = 2;
    public const int SW_SHOWMAXIMIZED = 3;
    public const int SW_MAXIMIZE = 3;
    public const int SW_SHOWNOACTIVATE = 4;
    public const int SW_SHOW = 5;
    public const int SW_MINIMIZE = 6;
    public const int SW_SHOWMINNOACTIVE = 7;
    public const int SW_SHOWNA = 8;
    public const int SW_RESTORE = 9;
    public const int SW_SHOWDEFAULT = 10;
    public const int SW_FORCEMINIMIZE = 11;
    public const int SW_MAX = 11;
    public const int AW_HOR_POSITIVE = 1;
    public const int AW_HOR_NEGATIVE = 2;
    public const int AW_VER_POSITIVE = 4;
    public const int AW_VER_NEGATIVE = 8;
    public const int AW_CENTER = 16;
    public const int AW_HIDE = 65536;
    public const int AW_ACTIVATE = 131072;
    public const int AW_SLIDE = 262144;
    public const int AW_BLEND = 524288;
    public const int SW_SCROLLCHILDREN = 1;
    public const int SW_INVALIDATE = 2;
    public const int SW_ERASE = 4;
    public const int SB_LINEUP = 0;
    public const int SB_LINELEFT = 0;
    public const int SB_LINEDOWN = 1;
    public const int SB_LINERIGHT = 1;
    public const int SB_PAGEUP = 2;
    public const int SB_PAGELEFT = 2;
    public const int SB_PAGEDOWN = 3;
    public const int SB_PAGERIGHT = 3;
    public const int SB_THUMBPOSITION = 4;
    public const int SB_THUMBTRACK = 5;
    public const int SB_TOP = 6;
    public const int SB_LEFT = 6;
    public const int SB_BOTTOM = 7;
    public const int SB_RIGHT = 7;
    public const int SB_ENDSCROLL = 8;
    public const int SB_HORZ = 0;
    public const int SB_VERT = 1;
    public const int SB_CTL = 2;
    public const int SB_BOTH = 3;
    public const int SIF_RANGE = 1;
    public const int SIF_PAGE = 2;
    public const int SIF_POS = 4;
    public const int SIF_DISABLENOSCROLL = 8;
    public const int SIF_TRACKPOS = 16;
    public const int SIF_ALL = 23;
    public const int WS_VISIBLE = 268435456;
    public const int WS_MINIMIZE = 536870912;
    public const int WS_CHILD = 1073741824;
    public const int WS_POPUP = -2147483648;
    public const int WS_CLIPCHILDREN = 33554432;
    public const int WS_CLIPSIBLINGS = 67108864;
    public const int WS_BORDER = 8388608;
    public const int WS_CAPTION = 12582912;
    public const int WS_VSCROLL = 2097152;
    public const int WS_HSCROLL = 1048576;
    public const int WS_SYSMENU = 524288;
    public const int WS_THICKFRAME = 262144;
    public const int WS_GROUP = 131072;
    public const int WS_TABSTOP = 65536;
    public const int WS_MINIMIZEBOX = 131072;
    public const int WS_MAXIMIZEBOX = 65536;
    public const int WS_POPUPWINDOW = -2138570752;
    public const int WS_EX_CLIENTEDGE = 512;
    public const int WS_EX_TRANSPARENT = 32;
    public const int WS_EX_WINDOWEDGE = 256;
    public const int WS_EX_TOOLWINDOW = 128;
    public const int WS_EX_TOPMOST = 8;
    public const int WS_EX_CONTROLPARENT = 65536;
    public const int WS_EX_APPWINDOW = 262144;
    public const int WS_EX_LAYERED = 524288;
    public const int WS_EX_NOACTIVATE = 134217728;
    public const int GWL_STYLE = -16;
    public const int GWL_EXSTYLE = -20;
    public const int GWL_HWNDPARENT = -8;
    public const int GW_OWNER = 4;
    public const int GW_CHILD = 5;
    public const int GA_PARENT = 1;
    public const int GA_ROOT = 2;
    public const int GA_ROOTOWNER = 3;
    public const int SWP_NOSIZE = 1;
    public const int SWP_NOMOVE = 2;
    public const int SWP_NOZORDER = 4;
    public const int SWP_NOOWNERZORDER = 512;
    public const int SWP_NOREDRAW = 8;
    public const int SWP_NOCOPYBITS = 256;
    public const int SWP_NOSENDCHANGING = 1024;
    public const int SWP_NOACTIVATE = 16;
    public const int SWP_FRAMECHANGED = 32;
    public const int SWP_SHOWWINDOW = 64;
    public const int SWP_HIDEWINDOW = 128;
    public const int RGN_AND = 1;
    public const int RGN_OR = 2;
    public const int RGN_XOR = 3;
    public const int RGN_DIFF = 4;
    public const int RGN_COPY = 5;
    public const int RGN_MIN = 1;
    public const int RGN_MAX = 5;
    public const int BS_SOLID = 0;
    public const int BS_NULL = 1;
    public const int BS_HOLLOW = 1;
    public const int BS_HATCHED = 2;
    public const int BS_PATTERN = 3;
    public const int BS_INDEXED = 4;
    public const int BS_DIBPATTERN = 5;
    public const int BS_DIBPATTERNPT = 6;
    public const int BS_PATTERN8X8 = 7;
    public const int BS_DIBPATTERN8X8 = 8;
    public const int BS_MONOPATTERN = 9;
    public const int DC_ACTIVE = 1;
    public const int DC_SMALLCAP = 2;
    public const int DC_ICON = 4;
    public const int DC_TEXT = 8;
    public const int DC_INBUTTON = 16;
    public const int DC_GRADIENT = 32;
    public const int DC_BUTTONS = 4096;
    public const int DFC_CAPTION = 1;
    public const int DFC_MENU = 2;
    public const int DFC_SCROLL = 3;
    public const int DFC_BUTTON = 4;
    public const int DFCS_CAPTIONCLOSE = 0;
    public const int DFCS_CAPTIONMIN = 1;
    public const int DFCS_CAPTIONMAX = 2;
    public const int DFCS_CAPTIONRESTORE = 3;
    public const int DFCS_CAPTIONHELP = 4;
    public const int DFCS_INACTIVE = 256;
    public const int DFCS_PUSHED = 512;
    public const int DFCS_CHECKED = 1024;
    public const int BDR_RAISEDOUTER = 1;
    public const int BDR_SUNKENOUTER = 2;
    public const int BDR_RAISEDINNER = 4;
    public const int BDR_SUNKENINNER = 8;
    public const int BF_LEFT = 1;
    public const int BF_TOP = 2;
    public const int BF_RIGHT = 4;
    public const int BF_BOTTOM = 8;
    public const int BF_RECT = 15;
    public const int HTNOWHERE = 0;
    public const int HTCLIENT = 1;
    public const int HTCAPTION = 2;
    public const int HTSYSMENU = 3;
    public const int HTLEFT = 10;
    public const int HTRIGHT = 11;
    public const int HTTOP = 12;
    public const int HTTOPLEFT = 13;
    public const int HTTOPRIGHT = 14;
    public const int HTBOTTOM = 15;
    public const int HTBOTTOMLEFT = 16;
    public const int HTBOTTOMRIGHT = 17;
    public const int HTBORDER = 18;
    public const int HTTRANSPARENT = -1;
    public const int HTMINBUTTON = 8;
    public const int HTMAXBUTTON = 9;
    public const int HTCLOSE = 20;
    public const int LWA_COLORKEY = 1;
    public const int LWA_ALPHA = 2;
    public const int ULW_COLORKEY = 1;
    public const int ULW_ALPHA = 2;
    public const int ULW_OPAQUE = 4;
    public const int RDW_INVALIDATE = 1;
    public const int RDW_INTERNALPAINT = 2;
    public const int RDW_ERASE = 4;
    public const int RDW_VALIDATE = 8;
    public const int RDW_NOINTERNALPAINT = 16;
    public const int RDW_NOERASE = 32;
    public const int RDW_NOCHILDREN = 64;
    public const int RDW_ALLCHILDREN = 128;
    public const int RDW_UPDATENOW = 256;
    public const int RDW_ERASENOW = 512;
    public const int RDW_FRAME = 1024;
    public const int RDW_NOFRAME = 2048;
    public const int GDI_SRCCOPY = 13369376;
    public const int GDI_PATINVERT = 5898313;
    public const int AC_SRC_OVER = 0;
    public const int AC_SRC_ALPHA = 1;
    public const int SM_CXSMSIZE = 52;
    public const int SM_CYSMSIZE = 53;
    public const int SPI_GETNONCLIENTMETRICS = 41;
    public const int SPI_GETMENUANIMATION = 4098;
    public const int SPI_GETMENUSHOWDELAY = 106;
    public const int SPI_GETMENUFADE = 4114;
    public const int SPI_GETDROPSHADOW = 4132;
    public const int SPI_GETKEYBOARDCUES = 4106;
    public const int SPI_GETTOOLTIPANIMATION = 4118;
    public const int COLOR_ACTIVECAPTION = 2;
    public const int COLOR_INACTIVECAPTION = 3;
    public const int COLOR_GRADIENTACTIVECAPTION = 27;
    public const int COLOR_GRADIENTINACTIVECAPTION = 28;
    public const int WP_CAPTION = 1;
    public const int WP_SMALLCAPTION = 2;
    public const int WP_MAXCAPTION = 5;
    public const int WP_MINBUTTON = 15;
    public const int WP_FRAMELEFT = 7;
    public const int WP_FRAMERIGHT = 8;
    public const int WP_FRAMEBOTTOM = 9;
    public const int WP_SMALLFRAMELEFT = 7;
    public const int WP_SMALLFRAMERIGHT = 8;
    public const int WP_SMALLFRAMEBOTTOM = 9;
    public const int WP_MDIMINBUTTON = 16;
    public const int WP_MDICLOSEBUTTON = 20;
    public const int WP_MDIRESTOREBUTTON = 22;
    public const int WP_SYSBUTTON = 13;
    public const int WP_CLOSEBUTTON = 18;
    public const int WP_SMALLCLOSEBUTTON = 19;
    public const int CP_DROPDOWNBUTTON = 1;
    public const int CBXS_DISABLED = 4;
    public const int CBXS_HOT = 2;
    public const int CBXS_NORMAL = 1;
    public const int CBXS_PRESSED = 3;
    public const int SPNP_DOWN = 2;
    public const int DNS_NORMAL = 1;
    public const int DNS_HOT = 2;
    public const int DNS_PRESSED = 3;
    public const int DNS_DISABLED = 4;
    public const int SPNP_UP = 1;
    public const int UNS_NORMAL = 1;
    public const int UNS_HOT = 2;
    public const int UNS_PRESSED = 3;
    public const int UNS_DISABLED = 4;
    public const int CBXS_FOCUSED = 5;
    public const int TTP_CLOSE = 5;
    public const int TTCS_NORMAL = 1;
    public const int TTCS_HOT = 2;
    public const int TTCS_PRESSED = 3;
    public const int TABP_TABITEM = 1;
    public const int TABP_TOPTABITEM = 5;
    public const int TIS_NORMAL = 1;
    public const int TIS_HOT = 2;
    public const int TIS_SELECTED = 3;
    public const int TIS_DISABLED = 4;
    public const int BP_PUSHBUTTON = 1;
    public const int BP_RADIOBUTTON = 2;
    public const int BP_CHECKBOX = 3;
    public const int BP_GROEPBOX = 4;
    public const int BP_USERBUTTON = 5;
    public const int MP_MENUITEM = 1;
    public const int MP_MENUDROPDOWN = 2;
    public const int MP_MENUBARITEM = 3;
    public const int MP_MENUBARDROPDOWN = 4;
    public const int MP_CHEVRON = 5;
    public const int MP_SEPARATOR = 6;
    public const int CS_ACTIVE = 1;
    public const int CS_INACTIVE = 2;
    public const int FS_ACTIVE = 1;
    public const int FS_INACTIVE = 2;
    public const int CBS_NORMAL = 1;
    public const int CBS_HOT = 2;
    public const int CBS_PUSHED = 3;
    public const int CBS_DISABLED = 4;
    public const int CBS_INACTIVE = 5;
    public const int PBS_NORMAL = 1;
    public const int PBS_HOT = 2;
    public const int PBS_PUSHED = 3;
    public const int PBS_DISABLED = 4;
    public const int PBS_DEFAULTED = 5;
    public const int DT_TOP = 0;
    public const int DT_LEFT = 0;
    public const int DT_CENTER = 1;
    public const int DT_RIGHT = 2;
    public const int DT_VCENTER = 4;
    public const int DT_BOTTOM = 8;
    public const int DT_WORDBREAK = 16;
    public const int DT_SINGLELINE = 32;
    public const int DT_EXPANDTABS = 64;
    public const int DT_NOCLIP = 256;
    public const int DT_CALCRECT = 1024;
    public const int DT_NOPREFIX = 2048;
    public const int DT_HIDEPREFIX = 1048576;
    public const int DT_PATH_ELLIPSIS = 16384;
    public const int DT_END_ELLIPSIS = 32768;
    public const int DT_WORD_ELLIPSIS = 262144;
    public const int TMT_STRING = 201;
    public const int TMT_INT = 202;
    public const int TMT_BOOL = 203;
    public const int TMT_COLOR = 204;
    public const int TMT_MARGINS = 205;
    public const int TMT_FILENAME = 206;
    public const int TMT_SIZE = 207;
    public const int TMT_POSITION = 208;
    public const int TMT_RECT = 209;
    public const int TMT_FONT = 210;
    public const int TMT_INTLIST = 211;
    public const int TMT_WIDTH = 2416;
    public const int TMT_HEIGHT = 2417;
    public const int TMT_CAPTIONFONT = 801;
    public const int TMT_SMALLCAPTIONFONT = 802;
    public const int TMT_MENUFONT = 803;
    public const int TMT_STATUSFONT = 804;
    public const int TMT_MSGBOXFONT = 805;
    public const int TMT_ICONTITLEFONT = 806;
    public const int TMT_ACTIVECAPTION = 1603;
    public const int TMT_BTNFACE = 1616;
    public const int TMT_BTNSHADOW = 1617;
    public const int TMT_BTNHIGHLIGHT = 1621;
    public const int TMT_HOTTRACKING = 1627;
    public const int TMT_FILLCOLOR = 3802;
    public const int TMT_GRADIENTCOLOR1 = 3810;
    public const int TMT_GRADIENTCOLOR2 = 3811;
    public const int TMT_GRADIENTCOLOR3 = 3812;
    public const int TMT_GRADIENTCOLOR4 = 3813;
    public const int TMT_GRADIENTCOLOR5 = 3814;
    public const int DTBG_CLIPRECT = 1;
    public const int DTBG_DRAWSOLID = 2;
    public const int DTBG_OMITBORDER = 4;
    public const int DTBG_OMITCONTENT = 8;
    public const int TRANSPARENT = 1;
    public const int OPAQUE = 2;
    public const uint SHACF_AUTOSUGGEST_FORCE_ON = 268435456;
    public const uint SHACF_AUTOSUGGEST_FORCE_OFF = 536870912;
    public const uint SHACF_AUTOAPPEND_FORCE_ON = 1073741824;
    public const uint SHACF_AUTOAPPEND_FORCE_OFF = 2147483648;
    public const uint SHACF_DEFAULT = 0;
    public const uint SHACF_FILESYSTEM = 1;
    public const uint SHACF_URLALL = 6;
    public const uint SHACF_URLHISTORY = 2;
    public const uint SHACF_URLMRU = 4;
    public const uint SHACF_USETAB = 8;
    public const uint SHACF_FILESYS_ONLY = 16;
    public const int KF_UP = 32768;
    public const int WH_GETMESSAGE = 3;
    public const int WH_KEYBOARD = 2;
    public const int WH_KEYBOARD_LL = 13;
    public const int WH_MIN = -1;
    public const int WH_MSGFILTER = -1;
    public const int WH_JOURNALRECORD = 0;
    public const int WH_JOURNALPLAYBACK = 1;
    public const int WH_CALLWNDPROC = 4;
    public const int WH_CBT = 5;
    public const int WH_SYSMSGFILTER = 6;
    public const int WH_MOUSE = 7;
    public const int WH_MOUSE_LL = 14;
    public const int HC_ACTION = 0;
    public const int HC_NOREMOVE = 3;
    public const int TBIF_IMAGE = 1;
    public const int TBIF_TEXT = 2;
    public const int TBIF_STATE = 4;
    public const int TBIF_STYLE = 8;
    public const int TBIF_LPARAM = 16;
    public const int TBIF_COMMAND = 32;
    public const int TBIF_SIZE = 64;
    public const int TBIF_BYINDEX = -2147483648;
    public const int GDC_LOGPIXELSX = 88;
    public const int GDC_LOGPIXELSY = 90;

    private NativeMethods()
    {
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool MessageBeep(int type);

    [DllImport("ole32.dll", PreserveSig = false)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public static extern object CoCreateInstance(
      [In] ref Guid clsid,
      [MarshalAs(UnmanagedType.Interface)] object punkOuter,
      int context,
      [In] ref Guid iid);

    [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SHAutoComplete(IntPtr hwndEdit, uint dwFlags);

    [DllImport("User32.dll")]
    public static extern bool ReleaseCapture();

    [DllImport("User32.dll")]
    public static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern IntPtr GetFocus();

    [DllImport("user32.dll")]
    public static extern IntPtr SetFocus(IntPtr hWnd);

    [DllImport("User32.dll")]
    public static extern bool ClientToScreen(IntPtr hWnd, ref NativeMethods.POINT lpPoint);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool ReadProcessMemory(
      IntPtr hProcess,
      IntPtr lpBaseAddress,
      IntPtr lpBuffer,
      int nSize,
      ref int lpNumberOfBytesWritten);

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, ref int lpdwProcessId);

    [DllImport("user32.dll")]
    public static extern bool GetClientRect(IntPtr hWnd, ref NativeMethods.RECT lpRect);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr OpenProcess(
      int dwDesiredAccess,
      bool bInheritHandle,
      int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr VirtualAllocEx(
      IntPtr hProcess,
      IntPtr lpAddress,
      int dwSize,
      int flAllocationType,
      int flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool VirtualFreeEx(
      IntPtr hProcess,
      IntPtr lpAddress,
      int dwSize,
      int dwFreeType);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindowEx(
      IntPtr hwndParent,
      IntPtr hwndChildAfter,
      StringBuilder lpszClass,
      StringBuilder lpszWindow);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(StringBuilder lpszClass, StringBuilder lpszWindow);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool GetUpdateRect(IntPtr hWnd, ref NativeMethods.RECT lpRect, bool erase);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr BeginPaint(IntPtr hWnd, ref NativeMethods.PAINTSTRUCT lpPaint);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool EndPaint(IntPtr hWnd, ref NativeMethods.PAINTSTRUCT lpPaint);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SetTimer(
      IntPtr hWnd,
      IntPtr nIDEvent,
      uint uElapse,
      QTimerCallbackDelegate lpTimerFunc);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr KillTimer(IntPtr hWnd, IntPtr nIDEvent);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetIconInfo(IntPtr hIcon, ref NativeMethods.ICONINFO iconInfo);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SetWindowsHookEx(
      int idHook,
      NativeMethods.HookProc lpfn,
      IntPtr hMod,
      int dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool UnhookWindowsHookEx(IntPtr hhook);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr CallNextHookEx(
      IntPtr hhk,
      int nCode,
      IntPtr wParam,
      IntPtr lParam);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    public static extern int GetCurrentThreadId();

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool TrackMouseEvent(ref NativeMethods.TRACKMOUSEEVENT lpEventTrack);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetLayeredWindowAttributes(
      IntPtr hwnd,
      int crKey,
      byte bAlpha,
      int dwFlags);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool UpdateLayeredWindow(
      IntPtr hwnd,
      IntPtr hdcDst,
      ref NativeMethods.POINT pptDst,
      ref NativeMethods.SIZE psize,
      IntPtr hdcSrc,
      ref NativeMethods.POINT pprSrc,
      int crKey,
      ref NativeMethods.BLENDFUNCTION pblend,
      int dwFlags);

    [DllImport("Msimg32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AlphaBlend(
      IntPtr hdcDest,
      int nXOriginDest,
      int nYOriginDest,
      int nWidthDest,
      int nHeightDest,
      IntPtr hdcSrc,
      int nXOriginSrc,
      int nYOriginSrc,
      int nWidthSrc,
      int nHeightSrc,
      NativeMethods.BLENDFUNCTION blendFunction);

    [DllImport("user32.dll")]
    public static extern bool AdjustWindowRectEx(
      ref NativeMethods.RECT lpRect,
      int dwStyle,
      bool bMenu,
      int dwExStyle);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetCaretPos(int X, int Y);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool HideCaret(IntPtr hWnd);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ShowCaret(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetScrollBarInfo(
      IntPtr hWnd,
      uint idObject,
      ref NativeMethods.SCROLLBARINFO psbi);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetScrollInfo(
      IntPtr hWnd,
      int fnBar,
      [MarshalAs(UnmanagedType.Struct)] ref NativeMethods.SCROLLINFO lpsi);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetScrollInfo(
      IntPtr hWnd,
      int fnBar,
      [MarshalAs(UnmanagedType.Struct)] ref NativeMethods.SCROLLINFO lpsi,
      [MarshalAs(UnmanagedType.Bool)] bool fRedraw);

    [DllImport("user32.dll")]
    public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, int bRedraw);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SystemParametersInfo(
      int uiAction,
      int uiParam,
      IntPtr pvParam,
      int fWinIni);

    [DllImport("kernel32.dll")]
    public static extern int GetLastError();

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    public static extern int ExcludeClipRect(
      IntPtr hdc,
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    public static extern int IntersectClipRect(
      IntPtr hdc,
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect);

    [DllImport("gdi32.dll")]
    public static extern uint GetLayout(IntPtr hdc);

    [DllImport("gdi32.dll")]
    public static extern uint SetLayout(IntPtr hdc, uint dwLayout);

    [DllImport("gdi32.dll")]
    public static extern int SetMapMode(IntPtr hdc, int fnMapMode);

    [DllImport("gdi32.dll")]
    public static extern int GetGraphicsMode(IntPtr hdc);

    [DllImport("gdi32.dll")]
    public static extern int SetGraphicsMode(IntPtr hdc, int iMode);

    [DllImport("gdi32.dll")]
    public static extern bool SetWindowOrgEx(
      IntPtr hdc,
      int X,
      int Y,
      ref NativeMethods.POINT lpPoint);

    [DllImport("gdi32.dll")]
    public static extern bool SetWindowExtEx(
      IntPtr hdc,
      int X,
      int Y,
      ref NativeMethods.POINT lpPoint);

    [DllImport("gdi32.dll")]
    public static extern uint OffsetViewportOrgEx(
      IntPtr hdc,
      int nXOffset,
      int nYOffset,
      ref NativeMethods.POINT lpPoint);

    [DllImport("gdi32.dll")]
    public static extern int OffsetWindowOrgEx(
      IntPtr hdc,
      int nXOffset,
      int nYOffset,
      ref NativeMethods.POINT lpPoint);

    [DllImport("gdi32.dll")]
    public static extern int SetViewportOrgEx(
      IntPtr hdc,
      int nXOffset,
      int nYOffset,
      ref NativeMethods.POINT lpPoint);

    [DllImport("gdi32.dll")]
    public static extern int SetViewportExtEx(
      IntPtr hdc,
      int nXOffset,
      int nYOffset,
      ref NativeMethods.POINT lpPoint);

    [DllImport("gdi32.dll")]
    public static extern int GetMapMode(IntPtr hdc);

    [DllImport("gdi32.dll")]
    public static extern uint SetTextAlign(IntPtr hdc, uint fMode);

    [DllImport("gdi32.dll")]
    public static extern uint GetTextAlign(IntPtr hdc);

    [DllImport("gdi32.dll")]
    public static extern int SetWorldTransform(IntPtr tmp_hDC, ref NativeMethods.XFORM lpXform);

    [DllImport("gdi32.dll")]
    public static extern int ModifyWorldTransform(
      IntPtr tmp_hDC,
      ref NativeMethods.XFORM lpXform,
      uint iMode);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool BitBlt(
      IntPtr hdcDest,
      int nXDest,
      int nYDest,
      int nWidth,
      int nHeight,
      IntPtr hdcSrc,
      int nXSrc,
      int nYSrc,
      int dwRop);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool MaskBlt(
      IntPtr hdcDest,
      int nXDest,
      int nYDest,
      int nWidth,
      int nHeight,
      IntPtr hdcSrc,
      int nXSrc,
      int nYSrc,
      IntPtr hbmMask,
      int xMask,
      int yMask,
      uint dwRop);

    [DllImport("gdi32.dll")]
    public static extern int DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteDC(IntPtr hDC);

    [DllImport("gdi32.dll")]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

    [DllImport("gdiplus.dll", CharSet = CharSet.Unicode)]
    internal static extern int GdipCreateBitmapFromScan0(
      int width,
      int height,
      int stride,
      int format,
      HandleRef scan0,
      out IntPtr bitmap);

    [DllImport("gdiplus.dll", CharSet = CharSet.Unicode)]
    internal static extern int GdipCreateHBITMAPFromBitmap(
      HandleRef nativeBitmap,
      out IntPtr hbitmap,
      int argbBackground);

    [DllImport("gdiplus.dll", CharSet = CharSet.Unicode)]
    internal static extern int GdipGetDC(HandleRef graphics, out IntPtr hdc);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr CreateBitmap(
      int nWidth,
      int nHeight,
      int nPlanes,
      int nBitsPerPixel,
      [MarshalAs(UnmanagedType.LPArray)] short[] lpvBits);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetTextMetrics(IntPtr hdc, IntPtr lptm);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetTextExtentPoint32(
      IntPtr hdc,
      string lpString,
      int cbString,
      ref NativeMethods.SIZE lpSize);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetTextExtentExPoint(
      IntPtr hdc,
      string lpszStr,
      int cchString,
      int nMaxExtent,
      ref short lpnFit,
      IntPtr alpDx,
      ref NativeMethods.SIZE lpSize);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool TextOut(
      IntPtr hdc,
      int nXStart,
      int nYStart,
      string lpString,
      int cbString);

    [DllImport("user32.dll")]
    private static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);

    [DllImport("user32.dll")]
    private static extern long GetKeyboardLayoutName(StringBuilder pwszKLID);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern short VkKeyScan(char ch);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int MapVirtualKey(int uCode, int uMapType);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    public static extern int DrawText(
      IntPtr hdc,
      string lpString,
      int cbString,
      ref NativeMethods.RECT lpRect,
      int uFormat);

    [DllImport("gdi32.dll")]
    public static extern int SetBkColor(IntPtr hdc, int crColor);

    [DllImport("gdi32.dll")]
    public static extern int SetTextColor(IntPtr hdc, int crColor);

    [DllImport("gdi32.dll")]
    public static extern int SetBkMode(IntPtr hdc, int iBkMode);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr CreateFontIndirect(ref NativeMethods.LOGFONT lplf);

    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowRect(IntPtr hWnd, out NativeMethods.RECT lpRect);

    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowPlacement(
      IntPtr hWnd,
      ref NativeMethods.WINDOWPLACEMENT lpwndpl);

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool BringWindowToTop(IntPtr hWnd);

    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetWindowPos(
      IntPtr hWnd,
      IntPtr hWndInsertAfter,
      int iX,
      int iY,
      int cX,
      int cY,
      uint uFlags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetAncestor(IntPtr hWnd, uint gaFlags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdc, int nFlags);

    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("User32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool RedrawWindow(
      IntPtr hWnd,
      IntPtr lprcUpdate,
      IntPtr hrgnUpdate,
      int flags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool InvalidateRect(
      IntPtr hWnd,
      ref NativeMethods.RECT lpRect,
      bool bErase);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool InvalidateRect(IntPtr hWnd, IntPtr rectangle, bool bErase);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool ValidateRect(IntPtr hWnd, ref NativeMethods.RECT lpRect);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool ValidateRect(IntPtr hWnd, IntPtr rectangle);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      IntPtr hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendNotifyMessage(
      IntPtr hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool WaitMessage();

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool TranslateMessage(ref NativeMethods.MSG msg);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DispatchMessage(ref NativeMethods.MSG msg);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool UpdateWindow(IntPtr hwnd);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetMessage(
      ref NativeMethods.MSG msg,
      int hWnd,
      uint wFilterMin,
      uint wFilterMax);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PeekMessage(
      ref NativeMethods.MSG msg,
      int hWnd,
      uint wFilterMin,
      uint wFilterMax,
      uint wFlag);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MsgWaitForMultipleObjects(
      int nCount,
      int pHandles,
      bool fWaitAll,
      int dwMilliseconds,
      int dwWakeMask);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DrawFrameControl(
      IntPtr hdc,
      ref NativeMethods.RECT lprc,
      int uType,
      int uState);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DrawEdge(
      IntPtr hdc,
      ref NativeMethods.RECT qrc,
      int edge,
      int grfFlags);

    [DllImport("user32.dll")]
    public static extern int GetSysColor(int nIndex);

    [DllImport("UxTheme.dll")]
    public static extern int DrawThemeEdge(
      IntPtr hTheme,
      IntPtr hdc,
      int iPartId,
      int iStateId,
      ref NativeMethods.RECT pDestRect,
      int uEdge,
      int uFlags,
      ref NativeMethods.RECT pContentRect);

    [DllImport("UxTheme.dll")]
    public static extern int DrawThemeBackground(
      IntPtr hTheme,
      IntPtr hdc,
      int iPartId,
      int iStateId,
      ref NativeMethods.RECT pRect,
      ref NativeMethods.RECT pClipRect);

    [DllImport("UxTheme.dll")]
    public static extern int DrawThemeBackgroundEx(
      IntPtr hTheme,
      IntPtr hdc,
      int iPartId,
      int iStateId,
      ref NativeMethods.RECT pRect,
      ref NativeMethods.DTBGOPTS pOptions);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    public static extern int GetCurrentThemeName(
      StringBuilder pszThemeFileName,
      int dwMaxNameChars,
      StringBuilder pszColorBuff,
      int cchMaxColorChars,
      StringBuilder pszSizeBuff,
      int cchMaxSizeChars);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr OpenThemeData(IntPtr hWnd, string ClassList);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    public static extern int GetThemeMetric(
      IntPtr hTheme,
      IntPtr hdc,
      int iPartId,
      int iStateId,
      int iPropId,
      ref int piVal);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    public static extern int GetThemeSysFont(
      IntPtr hTheme,
      int iIntID,
      out NativeMethods.LOGFONT pFont);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    public static extern int CloseThemeData(IntPtr hTheme);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int GetThemeMargins(
      IntPtr hTheme,
      IntPtr hdc,
      int iPartId,
      int iStateId,
      int iPropId,
      ref NativeMethods.RECT prc,
      ref NativeMethods.MARGINS pMargins);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int GetThemePartSize(
      IntPtr hTheme,
      IntPtr hdc,
      int iPartId,
      int iStateId,
      ref NativeMethods.RECT prc,
      NativeMethods.THEMESIZE eSize,
      ref NativeMethods.SIZE psz);

    [DllImport("gdi32.dll")]
    public static extern int CombineRgn(
      IntPtr hrgnDest,
      IntPtr hrgnSrc1,
      IntPtr hrgnSrc2,
      int fnCombineMode);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    public static extern bool Rectangle(
      IntPtr hdc,
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    public static extern IntPtr CreatePatternBrush(IntPtr hBitmap);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    public static extern IntPtr CreateSolidBrush(int crColor);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateBrushIndirect(ref NativeMethods.LOGBRUSH lplb);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateRectRgnIndirect(ref NativeMethods.RECT lprc);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect);

    [DllImport("gdi32.dll")]
    public static extern int GetClipBox(IntPtr hdc, ref NativeMethods.RECT lprc);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern IntPtr CancelDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PatBlt(
      IntPtr hdc,
      int nXLeft,
      int nYLeft,
      int nWidth,
      int nHeight,
      int dwRop);

    [DllImport("gdi32.dll")]
    public static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

    [DllImport("User32.dll")]
    public static extern IntPtr GetCapture();

    [DllImport("User32.dll")]
    public static extern bool EnumThreadWindows(
      uint dwThreadId,
      NativeMethods.EnumThreadWindowsCallBack lpfn,
      IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SetActiveWindow(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr WindowFromPoint(NativeMethods.POINT Point);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr ChildWindowFromPoint(
      IntPtr hWndParent,
      NativeMethods.POINT Point);

    [DllImport("user32.dll")]
    public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

    public struct MOUSEHOOKSTRUCT
    {
      public int pt_x;
      public int pt_y;
      public IntPtr hWnd;
      public int wHitTestCode;
      public int dwExtraInfo;
    }

    public struct MOUSEHOOKSTRUCTEX
    {
      public NativeMethods.MOUSEHOOKSTRUCT MOUSEHOOKSTRUCT;
      public uint mouseData;
    }

    public struct PAINTSTRUCT
    {
      public IntPtr hdc;
      public bool fErase;
      public int rcPaint_left;
      public int rcPaint_top;
      public int rcPaint_right;
      public int rcPaint_bottom;
      public bool fRestore;
      public bool fIncUpdate;
      public int reserved1;
      public int reserved2;
      public int reserved3;
      public int reserved4;
      public int reserved5;
      public int reserved6;
      public int reserved7;
      public int reserved8;
    }

    public struct XFORM
    {
      public float eM11;
      public float eM12;
      public float eM21;
      public float eM22;
      public float eDx;
      public float eDy;
    }

    public struct MSG : IDisposable
    {
      public IntPtr hwnd;
      public int message;
      public IntPtr wParam;
      public IntPtr lParam;
      public int time;
      public int pt_x;
      public int pt_y;

      public void Dispose()
      {
      }
    }

    public struct KBDLLHOOKSTRUCT
    {
      public int vkCode;
      public int scanCode;
      public int flags;
      public int time;
      public IntPtr dwExtraInfo;
    }

    public struct MINMAXINFO
    {
      public NativeMethods.POINT ptReserved;
      public NativeMethods.POINT ptMaxSize;
      public NativeMethods.POINT ptMaxPosition;
      public NativeMethods.POINT ptMinTrackSize;
      public NativeMethods.POINT ptMaxTrackSize;
    }

    public struct SCROLLBARINFO
    {
      public int cbSize;
      public NativeMethods.RECT rcScrollBar;
      public int dxyLineButton;
      public int xyThumbTop;
      public int xyThumbBottom;
      public int reserved;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
      public int[] rgstate;
    }

    public struct SCROLLINFO
    {
      public int cbSize;
      public int fMask;
      public int nMin;
      public int nMax;
      public int nPage;
      public int nPos;
      public int nTrackPos;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BLENDFUNCTION
    {
      public byte BlendOp;
      public byte BlendFlags;
      public byte SourceConstantAlpha;
      public byte AlphaFormat;
    }

    public struct DTBGOPTS
    {
      public int dwSize;
      public int dwFlags;
      public NativeMethods.RECT rcClip;
    }

    public struct RECT
    {
      public int left;
      public int top;
      public int right;
      public int bottom;

      public RECT(int iLeft, int iTop, int iWidth, int iHeight)
      {
        this.left = iLeft;
        this.top = iTop;
        this.right = iLeft + iWidth;
        this.bottom = iTop + iHeight;
      }
    }

    public struct POINT
    {
      public int x;
      public int y;

      public POINT(int ix, int iy)
      {
        this.x = ix;
        this.y = iy;
      }
    }

    public struct SIZE
    {
      public int cx;
      public int cy;

      public SIZE(int icx, int icy)
      {
        this.cx = icx;
        this.cy = icy;
      }
    }

    public struct WINDOWPLACEMENT
    {
      public int length;
      public int flags;
      public int showCmd;
      public NativeMethods.POINT ptMinPosition;
      public NativeMethods.POINT ptMaxPosition;
      public NativeMethods.RECT rcNormalPosition;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct LOGFONT
    {
      public int lfHeight;
      public int lfWidth;
      public int lfEscapement;
      public int lfOrientation;
      public int lfWeight;
      public byte lfItalic;
      public byte lfUnderline;
      public byte lfStrikeOut;
      public byte lfCharSet;
      public byte lfOutPrecision;
      public byte lfClipPrecision;
      public byte lfQuality;
      public byte lfPitchAndFamily;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string lfFaceName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct NONCLIENTMETRICS
    {
      public int cbSize;
      public int iBorderWidth;
      public int iScrollWidth;
      public int iScrollHeight;
      public int iCaptionWidth;
      public int iCaptionHeight;
      public NativeMethods.LOGFONT lfCaptionFont;
      public int iSmCaptionWidth;
      public int iSmCaptionHeight;
      public NativeMethods.LOGFONT lfSmCaptionFont;
      public int iMenuWidth;
      public int iMenuHeight;
      public NativeMethods.LOGFONT lfMenuFont;
      public NativeMethods.LOGFONT lfStatusFont;
      public NativeMethods.LOGFONT lfMessageFont;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TEXTMETRIC
    {
      public int tmHeight;
      public int tmAscent;
      public int tmDescent;
      public int tmInternalLeading;
      public int tmExternalLeading;
      public int tmAveCharWidth;
      public int tmMaxCharWidth;
      public int tmWeight;
      public int tmOverhang;
      public int tmDigitizedAspectX;
      public int tmDigitizedAspectY;
      public char tmFirstChar;
      public char tmLastChar;
      public char tmDefaultChar;
      public char tmBreakChar;
      public byte tmItalic;
      public byte tmUnderlined;
      public byte tmStruckOut;
      public byte tmPitchAndFamily;
      public byte tmCharSet;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WINDOWPOS : IDisposable
    {
      public IntPtr hwnd;
      public IntPtr hwndInsertAfter;
      public int x;
      public int y;
      public int cx;
      public int cy;
      public uint flags;

      public void Dispose()
      {
      }
    }

    public struct LOGBRUSH
    {
      public uint lbStyle;
      public uint lbColor;
      public uint lbHatch;
    }

    public struct NCCALCSIZE_PARAMS : IDisposable
    {
      public NativeMethods.RECT rgrc0;
      public NativeMethods.RECT rgrc1;
      public NativeMethods.RECT rgrc2;
      public IntPtr lppos;

      public void Dispose()
      {
      }
    }

    public struct TRACKMOUSEEVENT : IDisposable
    {
      public uint cbSize;
      public uint dwFlags;
      public IntPtr hwndTrack;
      public uint dwHoverTime;

      public void Dispose()
      {
      }
    }

    public struct MARGINS
    {
      public int cxLeftWidth;
      public int cxRightWidth;
      public int cyTopHeight;
      public int cyBottomHeight;
    }

    public enum THEMESIZE
    {
      TS_MIN,
      TS_TRUE,
      TS_DRAW,
    }

    public struct ICONINFO : IDisposable
    {
      public bool fIcon;
      public int xHotspot;
      public int yHotspot;
      public IntPtr hbmMask;
      public IntPtr hbmColor;

      public void Dispose()
      {
      }
    }

    public struct TBBUTTON
    {
      public int iBitmap;
      public int idCommand;
      public byte fsState;
      public byte fsStyle;
      public short bReserved;
      public IntPtr dwData;
      public IntPtr iString;
    }

    public struct TBBUTTONx64
    {
      public int iBitmap;
      public int idCommand;
      public byte fsState;
      public byte fsStyle;
      public short bReserved;
      public int iReserved2ForX64;
      public IntPtr dwData;
      public IntPtr iString;
    }

    internal delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

    public delegate bool EnumThreadWindowsCallBack(IntPtr hWnd, IntPtr lParam);
  }
}
