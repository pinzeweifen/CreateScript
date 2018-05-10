# Unity自动生成UI脚本

#### 项目介绍

无论是手动绑定对象还是代码绑定对象都过于繁琐，所以有了该插件

自动生成UGUI脚本，直接选择查找赋值或生成后从插件绑定对象

####插件将会生成 

5个文件夹：

    BuildController

    Controller

    BuildUI

    Model

    UI

5 个文件:

[可更改文件]

    Name_UI.cs
    
    Name_Model.cs
    
    Name_Controller.cs

[更新文件，通常这些文件是不更改的]

    Name_BuildUI.cs

    Name_BuildController.cs


#### 使用说明

![Image a](https://raw.githubusercontent.com/pinzeweifen/CreateScript/master/image/pulg.png)

生成脚本 [生成前必须选中 Hierarchy 面板对象]

更新脚本 [必须是从当前插件创建的脚本才能更新]

复制代码 [复制至剪切板]

挂载脚本 [必须先创建对应的UI文件才能挂在，无论是自己创建还是插件创建都可以]

绑定UI   [只有从当前插件创建的UI才能绑定，且必须先把脚本挂载到对象上]

全选变量     [选择生成所有子对象] 

全取消变量   [取消选择生成所有子对象]

全选属性器   [选择生成所有子对象属性器] 

全取消属性器 [取消选择生成所有子对象属性器]

全选事件     [选择生成所有子对象事件] 

全取消事件   [取消选择所有子对象事件]

全折叠       [隐藏所有子对象] 

全展开       [显示所有子对象]

查找赋值     [通过transform.Find查找对象] 

取消查找     [在 Hierarchy 面板绑定对象]

#### 土豪给点钱吧
![Image a](https://raw.githubusercontent.com/pinzeweifen/CreateScript/master/image/zhifubao.jpg)
![Image a](https://raw.githubusercontent.com/pinzeweifen/CreateScript/master/image/weixin.png)
