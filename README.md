# UEditor
UEditor是一个Unity编辑器扩展集，包含了一些实用的Unity编辑器扩展

怎样使用UEditor扩展集？

* 直接将本仓库克隆到你的Unity工程的Assets下的任意目录
* 或是仅将本仓库的Editor文件夹复制到你的Unity工程的Assets下的任意目录

扩展集中的扩展太多，有些扩展自己根本用不到，但还是出现在自己的编辑器中怎么办？

* 使用UEditor Window

### UEditor Window
通过菜单栏`Window/UEditor Window`打开
![](/Docs/ueditor_window.png)
通过UEditor Window可以配置启用/禁用哪些扩展，被禁用后的扩展将不再生效，也不会出现在编辑器中

**注意事项**
* 通过UEditor Window保存后，需要等待Unity自动重新编译后才会生效，大约10秒（不同版本的编辑器或不同的机器可能时间有所不同）
* 有些扩展启用/禁用状态改变后可能需要重启Unity编辑器才能保存生效（否则会有问题），所以请根据提示重启编辑器

### 扩展集
目前UEditor包含的扩展如下所示，**默认情况下所有扩展都处于禁用状态，需要通过UEditor Window打开**

* [打开编辑器所在目录](#打开编辑器所在目录)
* [打开工程所在目录](#打开工程所在目录)
* [清除无用资源](#清除无用资源)
* [查找重复资源](#查找重复资源)
* [根据自定义模板新建脚本并打开](#根据自定义模板新建脚本并打开)
* [将场景图像存储到Cubemap工具](#将场景图像存储到Cubemap工具)
* [在编辑器标题栏显示工程路径](#在编辑器标题栏显示工程路径)
* [程序集重新加载时是否弹出提示](#程序集重新加载时是否弹出提示)



#### 打开编辑器所在目录

File菜单扩展，位置`File/Open Editor Folder`

通过这个扩展可以直接在资源浏览器中打开编辑器所在目录

#### 打开工程所在目录

File菜单扩展，位置`File/File/Open Project Folder`

通过这个扩展可以直接在资源浏览器中打开工程所在目录

#### 清除无用资源

Assets菜单扩展，位置`Assets/Extend/Clear`

#### 查找重复资源

Assets菜单扩展，位置`Assets/Extend/Find Duplicate Resources`

#### 根据自定义模板新建脚本并打开

Assets菜单扩展，位置`Assets/Create/C# Stand Script`

使用自定义的模板创建新的脚本文件，创建完成后，自动在配置的代码编辑器中打开该文件

模板路径：`本仓库/Editor/Templates/NewScript.cs.txt`

#### 将场景图像存储到Cubemap工具

GameObject菜单扩展，位置`GameObject/Render into Cubemap`

使用这个扩展可以方便的将从指定位置观察到的场景图像存储到cubemap中

#### 在编辑器标题栏显示工程路径

Window菜单扩展，位置`Window/Show Project Path in Title`

有时候有多个工程的话，可能会打开多个Unity编辑器，这个时候就无法直观的看出哪个编辑器打开的是哪个工程
使用这个扩展可以在编辑器的标题栏加上工程路径的显示

#### 程序集重新加载时是否弹出提示
Unity默认会在脚本组件修改后自动重新编译脚本，过程中会造成Unity卡顿，但这个卡顿不是立即出现的，可能在你正操作Unity的时候就刚好出现了，给人的感觉就是操作突然被打断了，个人觉得还是有些难受的
所以添加了这个扩展，在程序集重新加载时弹出一个提示，好有个心里准备~