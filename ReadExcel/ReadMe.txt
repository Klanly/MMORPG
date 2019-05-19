CreateDataPath             生成的data文件所在路径
CreateCSOrLuaPath =        生成的脚本文件所在路径

NPCDataXinShouCun = NPCData

多张表存在相同的实体不同的数据 从Path.txt文件的第三行开始设置
格式为：{0} = {1}
{0}表示需要生成实体的表其他该类型的表跳过
{1}为该类型的表格所包含的字段 以及创建的实体名 {1}Entity
同类型表名必须包含{1}且其他类型表不包含{1}
如NPC表中有两张表NPCDataXinShouCun NPCDataChangAn
则会生成 NPCDataXinShouCunDBModel NPCDataChangAnDBModel NPCDataEntity 三个类
如果不写 NPCDataXinShouCun = NPCData
则会生成 NPCDataXinShouCunDBModel NPCDataChangAnDBModel NPCDataXinShouCunEntity NPCDataChangAnEntity 四个类