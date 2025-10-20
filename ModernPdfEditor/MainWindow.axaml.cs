using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System;
using System.Linq;

namespace ModernPdfEditor;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // Attach the event handler in code
        var openButton = this.FindControl<Button>("OpenButton");
        if (openButton != null)
        {
            openButton.Click += OpenButton_Click;
        }
    }

    private async void OpenButton_Click(object? sender, RoutedEventArgs e)
    {
        // Get top level from the current window.
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open PDF File",
            AllowMultiple = false,
            FileTypeFilter = new[] { FilePickerFileTypes.Pdf }
        });

        if (files.Count >= 1)
        {
            // Get the selected file's path and load it into the viewer.
            var pdfViewer = this.FindControl<AvaloniaPdfViewer.PdfViewer>("PdfViewer");
            if (pdfViewer != null)
            {
                // The AvaloniaPdfViewer control has a 'Source' property that takes the file path.
                var filePath = files[0].Path.AbsolutePath;
                pdfViewer.Source = filePath;
            }
        }
    }
}
