using System.Windows.Input;
using MVVMBase.ViewModel;
using MVVMBase.Command;

namespace MVVMExample2
{
    class MyViewModel1 : ViewModelBase
    {
        #region Left
        private int left;
        public int Left
        {
            get { return left; }
            set
            {
                if (value == left)
                    return;

                left = value;
                OnPropertyChanged("Left");
            }
        }

        #endregion

        #region Right
        private int right;
        public int Right
        {
            get { return right; }
            set
            {
                if (value == right)
                    return;

                right = value;
                OnPropertyChanged("Right");
            }
        }
        #endregion

        #region Result
        private double result;
        public double Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }
        #endregion


        #region Operations
        private string[] operations = new string[4] { "+", "-", "*", "/" };

        public string[] Operations
        {
            get
            {
                return operations;
            }
        }
        #endregion

        #region SelectedOperation
        private string selectedOperation;

        public string SelectedOperation
        {
            get { return selectedOperation; }
            set
            {
                if (selectedOperation == value)
                    return;

                selectedOperation = value;
                OnPropertyChanged("SelectedOperation");
            }
        }
        #endregion operation


        #region AddCommand
        private ICommand addCommand;

        public ICommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new AppCommand(
                    (object obj) =>
                    {
                        switch (selectedOperation)
                        {
                            case "+":
                                Result = Left + Right;
                                break;

                            case "-":
                                Result = Left - Right;
                                break;

                            case "*":
                                Result = (double)Left * (double)Right;
                                break;

                            case "/":
                                Result = (double)Left / (double)Right;
                                break;
                        }

                        OnPropertyChanged("Result");
                    },
                    (object obj) =>
                    {
                        return Left > 0 && Right > 0;
                    }
                ));
            }
        }
        #endregion
    }
}
