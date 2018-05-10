using System;
using System.Collections.Generic;

public interface ITreeNode
{
    ITreeNode Parent{get;set;}
    object Value{get;set;}
    ITreeNode this[int index]{get;}
    ITreeNode[] GetChilds();
    void AddChild(ITreeNode child);
    void RemoveChild(ITreeNode child);
    bool Task(Func<ITreeNode, bool> condition);
    void ForChilds(Action<int,ITreeNode> action);
    bool IsNotChilds();
    int GetChildsCount();
}

public class QTreeNode : ITreeNode
{
    private object value;
    private ITreeNode parent;
    private List<ITreeNode> childs = new List<ITreeNode>();
    public ITreeNode this[int index]{get{return childs[index];}} 
    public ITreeNode Parent { get { return parent; } set { parent = value; } }
    public object Value { get { return value; } set { this.value = value; } }
    public QTreeNode(ITreeNode parent = null) { this.parent = parent; }
    public void AddChild(ITreeNode child){ 
        if(child == this)
            QDebug.LogException("child is parent!");
        child.Parent = this; 
        childs.Add(child);
        }
    public ITreeNode[] GetChilds(){return childs.ToArray();}
    public void RemoveChild(ITreeNode child){child.Parent = null; childs.Remove(child);}
    public bool Task(Func<ITreeNode, bool> condition)
    {
        if(condition(this))return true;

        var count = childs.Count;
        for(int i=0;i<count;i++)
            if(childs[i].Task(condition)) 
                return true;
                
        return false;
    }

    public void ForChilds(Action<int, ITreeNode> action)
    {
        var count = childs.Count;
        for(int i=0;i<count;i++){
            action(i,childs[i]);
        }
    }

    public bool IsNotChilds()
    {
        return childs.Count == 0;
    }

    public int GetChildsCount()
    {
        return childs.Count;
    }
}
