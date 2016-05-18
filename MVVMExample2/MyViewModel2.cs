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
        public MyViewModel2()
        {
        }

        #region MyStringArray
        private ObservableCollection<string> myStringArray = new ObservableCollection<string>();

        public ObservableCollection<string> MyStringArray
        {
            get { return myStringArray; }
        }
        #endregion

        #region SelectedString
        private string selectedString;

        public string SelectedString
        {
            get { return selectedString; }
            set
            {
                if (selectedString == value)
                    return;

                selectedString = value;
                OnPropertyChanged("SelectedString");
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
                    myStringArray.Add(InputValue);
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
                    if (selectedString == null)
                        return;

                    myStringArray.Remove(selectedString);
                }));
            }
        }
        #endregion

        #region ShowCommand
        List<string> tempList = new List<string>();

        private ICommand showCommand;

        public ICommand ShowCommand
        {
            get
            {
                return showCommand ?? (showCommand = new AppCommand((object obj) =>
                {

                    //한번 우회해서 따로 ObservableCollection 담고 대입시키는 형식으로 객체 생성 피해야 할 듯.

                    var strList = new List<string>();
                    new Thread((object obj1) =>
                    {
                        for (int i = 0; i < 65535 * 32; i++)
                        {
                            strList.Add("asdf");
                        }

                        myStringArray = new ObservableCollection<string>(strList);
                        OnPropertyChanged("MyStringArray");
                    }).Start();
                }));
            }
        }
        #endregion
    }
}