using System.ComponentModel;

namespace AppDocumentManagement.UI.ViewModels
{
    /// <summary>
    /// Base class for ViewModels
    /// </summary>
    public class BaseViewModelClass : INotifyPropertyChanged
    {
        /// <summary>
        /// Declaring an event for property changes
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// Property change event callback function
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
