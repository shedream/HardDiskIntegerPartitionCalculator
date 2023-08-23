using System;
using System.IO;
using System.Reflection;
using System.Windows;

using PropertyChanged;

namespace HardDiskIntegerPartitionCalculator;

[AddINotifyPropertyChangedInterface]
public partial class MainWindow : Window
{
    private const double _cylinder = 7.84423828125;
    private string _inputPartitionStr = String.Empty;

    public static Version? Version => Assembly.GetExecutingAssembly().GetName().Version;

    public static string WindowTitle => $"HDIPC：磁盘整数分区计算器 v{Version?.Major}.{Version?.Minor} by SHE";

    public string About => $"👨‍💻Based on C#.NET 7.0, Released on {ReleasedLongTime}";

    public int InputPartition { get; set; }

    public string InputPartitionStr
    {
        get => _inputPartitionStr;
        set
        {
            value = value.Trim().TrimStart('0');

            if (String.IsNullOrEmpty(value))
            {
                _inputPartitionStr = value;

                InputPartition = 0;
            }

            if (Int32.TryParse(value, out int result) && result >= 0)
            {
                _inputPartitionStr = value;

                InputPartition = result;
            }
        }
    }

    public int OutputFAT32 => Math.Max(0, ((InputPartition - 1) * 4) + (1024 * InputPartition));
    public int OutputNTFS => Math.Max(0, (int)Math.Ceiling(((int)Math.Ceiling(InputPartition * 1024 / _cylinder)) * _cylinder));
    public string ReleasedLongTime => File.GetLastWriteTime(GetType().Assembly.Location).ToString("yyyy/MM/dd dddd tt HH:mm:ss.fff");

    public MainWindow()
    {
        InitializeComponent();
    }
}
