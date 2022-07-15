using Avalonia.Controls;
using Avalonia.Threading;
using DirTree.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace DirTree.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<DirectoryNode> RootCollection { get; }

        private async Task UnsetRoot()
        {
            void unsetRoot()
            {
                RootCollection.Clear();
            }

            await Dispatcher.UIThread.InvokeAsync(unsetRoot);
        }
        private async Task SetRoot(DirectoryNode root)
        {
            void setRoot()
            {
                RootCollection.Add(root);
            }

            await Dispatcher.UIThread.InvokeAsync(setRoot);
        }

        private bool browseButtonEnabled;
        public bool BrowseButtonEnabled
        {
            get => browseButtonEnabled;
            set => this.RaiseAndSetIfChanged(ref browseButtonEnabled, value);
        }
        private async Task DisableBrowseButton()
        {
            void disableButton()
            {
                BrowseButtonEnabled = false;
            }

            await Dispatcher.UIThread.InvokeAsync(disableButton);
        }
        private async Task EnableBrowseButton()
        {
            void enableButton()
            {
                BrowseButtonEnabled = true;
            }

            await Dispatcher.UIThread.InvokeAsync(enableButton);
        }

        private string message;
        public string Message
        {
            get => message;
            set => this.RaiseAndSetIfChanged(ref message, value);
        }
        private async Task SetMessage(string message)
        {
            void setMessage()
            {
                Message = message;
            }

            await Dispatcher.UIThread.InvokeAsync(setMessage);
        }

        public MainWindowViewModel()
        {
            RootCollection = new();
            browseButtonEnabled = true;
            message = "";
        }

        public void HandleBrowseButton(Window parent)
        {
            HandleBrowseButtonAsync(parent);
        }

        public async void HandleBrowseButtonAsync(Window parent)
        {
            await DisableBrowseButton();

            string? path = await GetFilePathFromBrowser(parent);

            if (path != null)
            {
                await SetMessage("Searching. . .");
                await UnsetRoot();
                var root = await DirectoryTree.BuildTreeAsync(path);
                await SetRoot(root);
            }

            await SetMessage("Done.");
            await EnableBrowseButton();
        }

        private async Task<string?> GetFilePathFromBrowser(Window parent)
        {
            var dialog = new OpenFolderDialog();

            return await dialog.ShowAsync(parent);
        }
    }
}
