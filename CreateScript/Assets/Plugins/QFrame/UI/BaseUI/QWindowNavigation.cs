using System.Collections.Generic;

public class QWindowNavigation
{
    private static Stack<QBaseWindow> stack = new Stack<QBaseWindow>();

    public static void ShowWindow(QBaseWindow window)
    {
        if (stack.Count > 0)
        {
            var peek = stack.Peek();
            if (peek == window) return;
            peek.gameObject.SetActive(false);
        }
        window.gameObject.SetActive(true);
        stack.Push(window);
    }

    public static void HideWindow()
    {
        if (stack.Count > 0)
        {
            var pop = stack.Pop();
            pop.gameObject.SetActive(false);
            stack.Peek().gameObject.SetActive(true);
        }
    }
}