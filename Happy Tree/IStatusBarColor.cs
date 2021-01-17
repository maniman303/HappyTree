namespace Happy_Tree
{
    public interface IStatusBarColor
    {
        void changestatuscolor(string color);
        void fullscreen(bool b);
        bool GetHasHardwareKeys();
    }
}