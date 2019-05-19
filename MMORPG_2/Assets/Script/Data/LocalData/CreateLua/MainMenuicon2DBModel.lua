require "Download/XLuaLogic/Data/Create/MainMenuIcon2Entity"

--数据访问
MainMenuIcon2DBModel = { }

local this = MainMenuIcon2DBModel;

local mainmenuicon2Table = { }; --定义表格

function MainMenuIcon2DBModel.New()
    return this;
end

function MainMenuIcon2DBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("MainMenuIcon2.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        mainmenuicon2Table[#mainmenuicon2Table+1] = MainMenuIcon2Entity.New( gameDataTable.Data[i][1], tonumber(gameDataTable.Data[i][2]), gameDataTable.Data[i][3], gameDataTable.Data[i][4], tonumber(gameDataTable.Data[i][5]), tonumber(gameDataTable.Data[i][6]), gameDataTable.Data[i][7], tonumber(gameDataTable.Data[i][8]),  );
    end

end

function MainMenuIcon2DBModel.GetList()
    return mainmenuicon2Table;
end

function MainMenuIcon2DBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #mainmenuicon2Table, 1 do
        if (mainmenuicon2Table[i].Id == id) then
            ret = mainmenuicon2Table[i];
            break;
        end
    end
    return ret;
end