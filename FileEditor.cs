using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ZapretLauncher
{
    public partial class MainWindow
    {
        string _filePath, _fileOriginal;
        bool _fileEditable, _fileSaving;

        bool CanCloseFile()
        {
            if (_modal != FilePanel) return true;
            if (_fileSaving) return false;
            if (_fileEditable && FileText.Text != _fileOriginal)
            {
                FileStatus.Text = "Есть несохранённые изменения. Нажмите «Сохранить» или «Отмена», чтобы закрыть без сохранения.";
                return false;
            }
            return true;
        }

        void OpenFilePanel(string path, string title, bool editable)
        {
            if (!CanCloseFile()) return;
            if (editable && (_state == UiState.Testing || _state == UiState.Starting || _state == UiState.Installing))
            {
                Log("Дождитесь завершения операции или отмените автоподбор перед редактированием списков");
                return;
            }
            string text = File.ReadAllText(path);
            _filePath = path;
            _fileEditable = editable;
            _fileOriginal = text;
            FileTitle.Text = title;
            FileHelp.Text = Path.GetFileName(path) + (editable
                ? " · По одной записи в строке. После сохранения работающий zapret перезапустится автоматически."
                : " · Просмотр журнала. Нажмите «Обновить», чтобы прочитать новые записи.");
            FileText.IsReadOnly = !editable;
            FileText.Text = text;
            FileText.MaxHeight = Math.Max(120, ActualHeight - 330);
            FileStatus.Text = editable
                ? "Домены указывайте без https:// и пути; IP — адресом или подсетью. Пустой список получит безопасную служебную запись."
                : "Журнал открыт только для чтения.";
            FileSave.Visibility = editable ? Visibility.Visible : Visibility.Collapsed;
            FileReload.Visibility = editable ? Visibility.Collapsed : Visibility.Visible;
            FileCancel.Content = editable ? "Отмена" : "Закрыть";
            OpenModal(FilePanel);
            FileText.Focus();
        }

        void FileText_Changed(object sender, TextChangedEventArgs e)
        {
            if (FileSave != null) FileSave.IsEnabled = !_fileSaving && _fileEditable && FileText.Text != _fileOriginal;
        }

        void FileResize_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FileText.Height = Math.Max(FileText.MinHeight, Math.Min(FileText.MaxHeight, FileText.ActualHeight + e.VerticalChange));
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (FileText != null) FileText.MaxHeight = Math.Max(120, ActualHeight - 330);
        }

        void FileCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_fileSaving) return;
            _fileOriginal = FileText.Text;
            CloseModal();
        }

        void FileReload_Click(object sender, RoutedEventArgs e)
        {
            try { FileText.Text = File.ReadAllText(_filePath); FileText.ScrollToEnd(); }
            catch (Exception ex) { FileStatus.Text = "Не удалось прочитать журнал: " + ex.Message; }
        }

        async void FileSave_Click(object sender, RoutedEventArgs e)
        {
            if (!_fileEditable || _fileSaving) return;
            if (_state == UiState.Testing || _state == UiState.Starting || _state == UiState.Installing)
            {
                FileStatus.Text = "Дождитесь завершения текущей операции перед сохранением.";
                return;
            }
            _fileSaving = true;
            FileSave.IsEnabled = FileCancel.IsEnabled = FileText.IsEnabled = false;
            bool saved = false;
            try
            {
                string text = FileText.Text;
                if (File.ReadAllText(_filePath) != _fileOriginal)
                    throw new IOException("Файл изменён другой программой. Скопируйте свои правки и откройте список заново.");
                _fileOriginal = UserListFile.Save(_filePath, text);
                saved = true;
                FileText.Text = _fileOriginal;
                foreach (var strategy in _strategies) strategy.ResetProbe();
                if (_state == UiState.Running)
                {
                    FileStatus.Text = "Сохранено. Перезапускаю zapret…";
                    SetState(UiState.Starting, "Применение списков");
                    await StopAsync();
                    await StartAsync();
                    FileStatus.Text = _state == UiState.Running
                        ? "Сохранено. Zapret перезапущен, изменения применены."
                        : "Файл сохранён, но zapret не запустился. Причина показана в статусе приложения.";
                }
                else FileStatus.Text = "Сохранено. Изменения применятся при следующем включении zapret.";
            }
            catch (Exception ex) { FileStatus.Text = (saved ? "Файл сохранён. Ошибка перезапуска: " : "Не удалось сохранить: ") + ex.Message; }
            finally
            {
                _fileSaving = false;
                FileCancel.IsEnabled = FileText.IsEnabled = true;
                FileSave.IsEnabled = FileText.Text != _fileOriginal;
            }
        }
    }
}
