using System;
using System.Windows;

using PropertyChanged;

namespace HardDiskIntegerPartitionCalculator;

[AddINotifyPropertyChangedInterface]
public partial class MainWindow : Window
{
    private const double _cylinder = 7.84423828125;

    public int InputPartition { get; set; } = 1;

    public int OutputFAT32 => ((InputPartition - 1) * 4) + (1024 * InputPartition);

    public int OutputNTFS => (int)Math.Ceiling(((int)Math.Ceiling(InputPartition * 1024 / _cylinder)) * _cylinder);

    public MainWindow()
    {
        InitializeComponent();
    }
}
