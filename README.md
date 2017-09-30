# uFrame_kbe

注意 
==
这还是个实验性项目, 不建议新手入坑, 不建议用于实际项目.

要用好这个框架,你需要以下的知识储备:

1.熟悉Unity3d

2.熟悉设计模式(观察者模式, 事件模式, 命令模式等)

3.熟悉MVC框架或MVVM框架

4.熟悉响应式编程RX ( Reactive Extensions ) 或 UniRX ( Reactive Extensions Unity3d版 )

学习uFrame可以通过Youtube搜索uFrame



YouTube视频教程
==
[uFrame_kbe教程（幻火项目移植记录）](https://www.youtube.com/watch?v=U92KrtGQ5Mc&list=PLS2_hNFiin5IKlV_yDac1wAH_3L12Mk_N)



更新（2017.09.11）
==
更新uFrame，更新UniRX。（Unity 2017） (Scripting Runtime Version: .Net 4.6)

![框架示例](https://raw.githubusercontent.com/m969/uFrame_kbe/master/uFrame_kbe04.png)

这些是uFrame的内部Command，不能再作为自定义Command，否则在生成代码时会出错。

之前
==
不知道uFrame的可以百度uFrame，不知道kbe的可以百度kbengine。

这个工程将uFrame的MVVM框架和kbengine的unity客户端插件结合，可以改善客户端的代码结构，提升开发效率。

在vs中，你可以通过全局代码搜索 uFrame_kbe 关键字，有 uFrame_kbe 关键字标志的地方都是为了使两个插件可以协同工作而增加或修改的代码。

两个插件的代码都有改动，主要是为了解决以下问题：

1 .实体类名及命名空间的变动（如 KBEngine.Avatar -> KbeBalls.AvatarViewModel ）

2 .多线程模式下属性更新和方法调用（kbe的 客户端方法调用 在uFrame的MVVM框架下会转变成 执行命令 ）（单线程/多线程模式下均可工作）

3 .将uFrame的事件机制和kbe的事件机制结合（两个事件机制均可接收kbe插件层抛出的事件并进行处理）

工程中有一个示例demo（Assets/_MagicFire/ProjectsCode/KbeBalls），是房间类demo：kbengine_unity3d_balls的uFrameMVVM重制版，服务端脚本没有做任何改动，可以直接使用原来的服务端资产。

要运行demo的话，先启动服务端，然后打开LoginScene直接运行即可。(unity版本2017)

![框架示例](https://raw.githubusercontent.com/m969/uFrame_kbe/master/uFrame_kbe01.png)
![框架示例](https://raw.githubusercontent.com/m969/uFrame_kbe/master/uFrame_kbe02.png)
![框架示例](https://raw.githubusercontent.com/m969/uFrame_kbe/master/uFrame_kbe03.png)
