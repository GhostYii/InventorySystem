# Unity 背包系统

一个可用于游戏项目的背包系统

#### 基本功能
* 使用UGUI实现，适用于Unity5 以上的版本
* 背包物品分类管理
* 背包容量限制
* 方便实现的背包仓库管理
* UI与代码分离
* 使用XML保存背包信息
* 可拓展

### 使用指南
你可以在你的项目中使用
```
<?xml version="1.0" encoding="utf-8"?>
<!--DO NOT CHANGE THE ORDER OF THE CHILD ITEM-->
<ItemMes>
  <Item ID="1001" type="item">
    <repeatable>false</repeatable>
    <levle>none</levle>
    <equipmentType>none</equipmentType>
    <maxSum>1</maxSum>
    <picName>item_pendant</picName>
  </Item>
</ItemMes>
```
这样的格式来定义属于你的ITEM格式。如果需要拓展，只需修改`ItemBase.cs`代码文件即可。


你也可以定义自己的类来定义属于自己的Item，只需继承`ItemBase.cs`即可被该背包系统识别。

相同的，你可以使用
```
<?xml version="1.0" encoding="utf-8"?>
<!--DO NOT CHANGE THE ORDER OF THE CHILD ITEM-->
<TooltipData>
  <tooltip ID="1004">
    <name>老坛</name>
    <introduction>一般来说，老坛里面都是酸菜
但是我不知道这个坛子里会是什么东西。
    </introduction>
    <effect>也许里面真的是酸菜</effect>
  </tooltip>
</TooltipData>
```
这样的格式来定义该物品的Tooltip，只需要Id对应即可。

如果你需要使用JSON文件而非XML，你可以使用项目中带的LitJson类库或者使用Unity5.4以上的版本自行修改格式。

### 相关API
使用`DataLoader.Instance.LoadItemData(string path)`来序列化Item信息

使用`DataLoader.Instance.LoadTooltipData(string path)`来序列化Tooltip信息

使用`ItemDataController.Instance.FindItemDataWithId`来查找已加载的Item信息

使用`ItemGrid.Instance.AddItem`来添加Item至背包

使用`ItemGrid.Instance.RemoveItem`来移除背包中的物体

使用`ItemGrid.Instance.CheckFull`来检查背包是否已满

使用`ItemGrid.Instance.SwapItemDataByDataId`来交换背包中的Item位置

### 问题
右键菜单可以自定义，但是实现方法不好，等一个有缘人重写。

![](https://github.com/GhostYii/InventorySystem/raw/master/Readme/Preview.png)

![](https://github.com/GhostYii/InventorySystem/raw/master/Readme/tooltip.png)

![](https://github.com/GhostYii/InventorySystem/raw/master/Readme/drag.png)

![](https://github.com/GhostYii/InventorySystem/raw/master/Readme/rightclick0.png)  ![](https://github.com/GhostYii/InventorySystem/raw/master/Readme/rightclick1.png)
