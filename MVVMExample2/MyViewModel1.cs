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
        private int result;
        public int Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }
        #endregion


        #region AddCommand
        private ICommand addCommand;

        public ICommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new AppCommand(
                    (object obj) =>
                    {
                        Result = Left + Right;
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
