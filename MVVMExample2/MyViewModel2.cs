using System.Collections.ObjectModel;
using MVVMBase.ViewModel;
using MVVMBase.Command;
using System.Windows.Input;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MVVMExample2
{
    class MyViewModel2 : ViewModelBase
    {
        #region ListItems
        private ObservableCollection<string> listItems = new ObservableCollection<string>();

        public ObservableCollection<string> ListItems
        {
            get { return listItems; }
        }
        #endregion

        #region ListSelectedItem
        private string listSelectedItem;

        public string ListSelectedItem
        {
            get { return listSelectedItem; }
            set
            {
                if (listSelectedItem == value)
                    return;

                listSelectedItem = value;
                OnPropertyChanged("ListSelectedItem");
            }
        }
        #endregion

        #region InputValue
        private string inputValue;

        public string InputValue
        {
            get { return inputValue; }
            set
            {
                if (inputValue == value)
                    return;

                inputValue = value;
                OnPropertyChanged("InputValue");
            }
        }
        #endregion

        #region AddCommand
        private ICommand addCommand;

        public ICommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new AppCommand((object obj) =>
                {
                    if (InputValue == null || InputValue == string.Empty)
                        return;

                    ListItems.Add(InputValue);
                    InputValue = "";
                }));
            }
        }
        #endregion

        #region RemoveCommand
        private ICommand removeCommand;

        public ICommand RemoveCommand
        {
            get
            {
                return removeCommand ?? (removeCommand = new AppCommand((object obj) =>
                {
                    if (InputValue == null)
                        return;

                    listItems.Remove(listSelectedItem);
                }));
            }
        }
        #endregion

        #region ShowCommand
        List<string> tempList = new List<string>();

        private ICommand asyncCommand;

        public ICommand AsyncCommand
        {
            get
            {
                return asyncCommand ?? (asyncCommand = new AppCommand((object obj) =>
                {
                    var cancellationTokenSource = new CancellationTokenSource();

                    List<string> tempList = new List<string>();
                    Task.Factory.StartNew((obj1) =>
                    {
                        Random r = new Random();

                        for (int i = 0; i < 999999; i++)
                        {
                            tempList.Add(r.Next(int.MinValue, int.MaxValue).ToString());
                        }

                        listItems = new ObservableCollection<string>(tempList);
                        OnPropertyChanged("ListItems");

                    }, TaskCreationOptions.LongRunning, cancellationTokenSource.Token);
                }));
            }
        }
        #endregion
    }
}