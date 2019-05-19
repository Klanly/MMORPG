require "Download/XLuaLogic/Data/Create/MainMenuIcon1Entity"

--数据访问
MainMenuIcon1DBModel = { }

local this = MainMenuIcon1DBModel;

local mainmenuicon1Table = { }; --定义表格

function MainMenuIcon1DBModel.New()
    return this;
end

function MainMenuIcon1DBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("MainMenuIcon1.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        mainmenuicon1Table[#mainmenuicon1Table+1] = MainMenuIcon1Entity.New( gameDataTable.Data[i][1], tonumber(gameDataTable.Data[i][2]), gameDataTable.Data[i][3], gameDataTable.Data[i][4], tonumber(gameDataTable.Data[i][5]), tonumber(gameDataTable.Data[i][6]), gameDataTable.Data[i][7], tonumber(gameDataTable.Data[i][8]) );
    end

end

function MainMenuIcon1DBModel.GetList()
    return mainmenuicon1Table;
end

function MainMenuIcon1DBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #mainmenuicon1Table, 1 do
        if (mainmenuicon1Table[i].Id == id) then
            ret = mainmenuicon1Table[i];
            break;
        end
    end
    return ret;
end