using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace ThemedWindowStyle
{
    public class ThemedWindow : System.Windows.Window
    {
        private Point cursorOffset;
        private double restoreTop;

        private FrameworkElement borderLeft;
        private FrameworkElement borderTopLeft;
        private FrameworkElement borderTop;
        private FrameworkElement borderTopRight;
        private FrameworkElement borderRight;
        private FrameworkElement borderBottomRight;
        private FrameworkElement borderBottom;
        private FrameworkElement borderBottomLeft;
        private FrameworkElement caption;
        private FrameworkElement frame;
        private Button minimizeButton;
        private Button maximizeButton;
        private Button closeButton;
        private IntPtr handle;

        public ThemedWindow()
        {
            SourceInitialized += (sender, e) =>
            {
                handle = new WindowInteropHelper(this).Handle;
                HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WndProc));
            };

            Style = (Style)TryFindResource("ThemedWindowStyle");
            ;

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RegisterFrame();
            RegisterBorders();
            RegisterCaption();
            RegisterMinimizeButton();
            RegisterMaximizeButton();
            RegisterCloseButton();
        }

        private void RegisterCloseButton()
        {
            closeButton = (Button)GetTemplateChild("PART_WindowCaptionCloseButton");

            if (closeButton != null)
            {
                closeButton.Click += (sender, e) => Close();
            }
        }

        private void RegisterMaximizeButton()
        {
            maximizeButton = (Button)GetTemplateChild("PART_WindowCaptionMaximizeButton");

            if (maximizeButton != null)
            {
                maximizeButton.Click += (sender, e) =>
                {
                    if (WindowState == System.Windows.WindowState.Normal)
                    {
                        WindowState = System.Windows.WindowState.Maximized;
                    }
                    else
                    {
                        WindowState = System.Windows.WindowState.Normal;
                    }
                };
            }
        }

        private void RegisterMinimizeButton()
        {
            minimizeButton = (Button)GetTemplateChild("PART_WindowCaptionMinimizeButton");

            if (minimizeButton != null)
            {
                minimizeButton.Click += (sender, e) => WindowState = System.Windows.WindowState.Minimized;
            }
        }

        private void RegisterBorderEvents(WindowBorderEdge borderEdge, FrameworkElement border)
        {
            
            border.MouseEnter += (sender, e) =>
            {
                if (WindowState != WindowState.Maximized && ResizeMode == ResizeMode.CanResize)
                {
                    switch (borderEdge)
                    {
                        case WindowBorderEdge.Left:
                        case WindowBorderEdge.Right:
                            border.Cursor = Cursors.SizeWE;
                            break;
                        case WindowBorderEdge.Top:
                        case WindowBorderEdge.Bottom:
                            border.Cursor = Cursors.SizeNS;
                            break;
                        case WindowBorderEdge.TopLeft:
                        case WindowBorderEdge.BottomRight:
                            border.Cursor = Cursors.SizeNWSE;
                            break;
                        case WindowBorderEdge.TopRight:
                        case WindowBorderEdge.BottomLeft:
                            border.Cursor = Cursors.SizeNESW;
                            break;
                    }
                }
                else
                {
                    border.Cursor = Cursors.Arrow;
                }
            };

            border.MouseLeftButtonDown += (sender, e) =>
            {
                if (WindowState != WindowState.Maximized && ResizeMode == ResizeMode.CanResize)
                {
                    Point cursorLocation = e.GetPosition(this);
                    Point cursorOffset = new Point();

                    switch (borderEdge)
                    {
                        case WindowBorderEdge.Left:
                            cursorOffset.X = cursorLocation.X;
                            break;
                        case WindowBorderEdge.TopLeft:
                            cursorOffset.X = cursorLocation.X;
                            cursorOffset.Y = cursorLocation.Y;
                            break;
                        case WindowBorderEdge.Top:
                            cursorOffset.Y = cursorLocation.Y;
                            break;
                        case WindowBorderEdge.TopRight:
                            cursorOffset.X = (Width - cursorLocation.X);
                            cursorOffset.Y = cursorLocation.Y;
                            break;
                        case WindowBorderEdge.Right:
                            cursorOffset.X = (Width - cursorLocation.X);
                            break;
                        case WindowBorderEdge.BottomRight:
                            cursorOffset.X = (Width - cursorLocation.X);
                            cursorOffset.Y = (Height - cursorLocation.Y);
                            break;
                        case WindowBorderEdge.Bottom:
                            cursorOffset.Y = (Height - cursorLocation.Y);
                            break;
                        case WindowBorderEdge.BottomLeft:
                            cursorOffset.X = cursorLocation.X;
                            cursorOffset.Y = (Height - cursorLocation.Y);
                            break;
                    }

                    this.cursorOffset = cursorOffset;

                    border.CaptureMouse();
                }
            };

            border.MouseMove += (sender, e) =>
            {
                if (WindowState != WindowState.Maximized && border.IsMouseCaptured && ResizeMode == ResizeMode.CanResize)
                {
                    Point cursorLocation = e.GetPosition(this);

                    double nHorizontalChange = (cursorLocation.X - cursorOffset.X);
                    double pHorizontalChange = (cursorLocation.X + cursorOffset.X);
                    double nVerticalChange = (cursorLocation.Y - cursorOffset.Y);
                    double pVerticalChange = (cursorLocation.Y + cursorOffset.Y);

                    switch (borderEdge)
                    {
                        case WindowBorderEdge.Left:
                            if (Width - nHorizontalChange <= MinWidth)
                                break;
                            Left += nHorizontalChange;
                            Width -= nHorizontalChange;
                            break;
                        case WindowBorderEdge.TopLeft:
                            if (Width - nHorizontalChange <= MinWidth)
                                break;
                            Left += nHorizontalChange;
                            Width -= nHorizontalChange;
                            if (Height - nVerticalChange <= MinHeight)
                                break;
                            Top += nVerticalChange;
                            Height -= nVerticalChange;
                            break;
                        case WindowBorderEdge.Top:
                            if (Height - nVerticalChange <= MinHeight)
                                break;
                            Top += nVerticalChange;
                            Height -= nVerticalChange;
                            break;
                        case WindowBorderEdge.TopRight:
                            if (pHorizontalChange <= MinWidth)
                                break;
                            Width = pHorizontalChange;
                            if (Height - nVerticalChange <= MinHeight)
                                break;
                            Top += nVerticalChange;
                            Height -= nVerticalChange;
                            break;
                        case WindowBorderEdge.Right:
                            if (pHorizontalChange <= MinWidth)
                                break;
                            Width = pHorizontalChange;
                            break;
                        case WindowBorderEdge.BottomRight:
                            if (pHorizontalChange <= MinWidth)
                                break;
                            Width = pHorizontalChange;
                            if (pVerticalChange <= MinHeight)
                                break;
                            Height = pVerticalChange;
                            break;
                        case WindowBorderEdge.Bottom:
                            if (pVerticalChange <= MinHeight)
                                break;
                            Height = pVerticalChange;
                            break;
                        case WindowBorderEdge.BottomLeft:
                            if (Width - nHorizontalChange <= MinWidth)
                                break;
                            Left += nHorizontalChange;
                            Width -= nHorizontalChange;
                            if (pVerticalChange <= MinHeight)
                                break;
                            Height = pVerticalChange;
                            break;
                    }
                }
            };

            border.MouseLeftButtonUp += (sender, e) =>
            {
                border.ReleaseMouseCapture();
            };
        }

        private void RegisterBorders()
        {
            borderLeft = (FrameworkElement)GetTemplateChild("PART_WindowBorderLeft");
            borderTopLeft = (FrameworkElement)GetTemplateChild("PART_WindowBorderTopLeft");
            borderTop = (FrameworkElement)GetTemplateChild("PART_WindowBorderTop");
            borderTopRight = (FrameworkElement)GetTemplateChild("PART_WindowBorderTopRight");
            borderRight = (FrameworkElement)GetTemplateChild("PART_WindowBorderRight");
            borderBottomRight = (FrameworkElement)GetTemplateChild("PART_WindowBorderBottomRight");
            borderBottom = (FrameworkElement)GetTemplateChild("PART_WindowBorderBottom");
            borderBottomLeft = (FrameworkElement)GetTemplateChild("PART_WindowBorderBottomLeft");

            RegisterBorderEvents(WindowBorderEdge.Left, borderLeft);
            RegisterBorderEvents(WindowBorderEdge.TopLeft, borderTopLeft);
            RegisterBorderEvents(WindowBorderEdge.Top, borderTop);
            RegisterBorderEvents(WindowBorderEdge.TopRight, borderTopRight);
            RegisterBorderEvents(WindowBorderEdge.Right, borderRight);
            RegisterBorderEvents(WindowBorderEdge.BottomRight, borderBottomRight);
            RegisterBorderEvents(WindowBorderEdge.Bottom, borderBottom);
            RegisterBorderEvents(WindowBorderEdge.BottomLeft, borderBottomLeft);
        }

        private void RegisterCaption()
        {
            caption = (FrameworkElement)GetTemplateChild("PART_WindowCaption");

            if (caption != null)
            {
                caption.MouseLeftButtonDown += (sender, e) =>
                {
                    restoreTop = e.GetPosition(this).Y;

                    if (e.ClickCount == 2 && e.ChangedButton == System.Windows.Input.MouseButton.Left && (ResizeMode != ResizeMode.CanMinimize && ResizeMode != ResizeMode.NoResize))
                    {
                        if (WindowState != System.Windows.WindowState.Maximized)
                        {
                            WindowState = System.Windows.WindowState.Maximized;
                        }
                        else
                        {
                            WindowState = System.Windows.WindowState.Normal;
                        }

                        return;
                    }

                    DragMove();
                };

                caption.MouseMove += (sender, e) =>
                {
                    if (e.LeftButton == MouseButtonState.Pressed && caption.IsMouseOver)
                    {
                        if (WindowState == WindowState.Maximized)
                        {
                            WindowState = WindowState.Normal;
                            Top = restoreTop - 10;
                            DragMove();
                        }
                    }
                };
            }
        }

        private void RegisterFrame()
        {
            frame = (FrameworkElement)GetTemplateChild("PART_WindowFrame");
        }

        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            IntPtr monitor = NativeMethods.MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != System.IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                NativeMethods.GetMonitorInfo(monitor, monitorInfo);

                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;

                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return IntPtr.Zero;
        }

        private enum WindowBorderEdge
        {
            Left,
            TopLeft,
            Top,
            TopRight,
            Right,
            BottomRight,
            Bottom,
            BottomLeft
        }
    }
}