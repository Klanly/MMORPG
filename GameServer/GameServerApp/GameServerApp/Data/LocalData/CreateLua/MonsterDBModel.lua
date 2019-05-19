require "Download/XLuaLogic/Data/Create/MonsterEntity"

--数据访问
MonsterDBModel = { }

local this = MonsterDBModel;

local monsterTable = { }; --定义表格

function MonsterDBModel.New()
    return this;
end

function MonsterDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Monster.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        monsterTable[#monsterTable+1] = MonsterEntity.New( gameDataTable.Data[i][1], tonumber(gameDataTable.Data[i][2]), tonumber(gameDataTable.Data[i][3]), tonumber(gameDataTable.Data[i][4]), tonumber(gameDataTable.Data[i][5]), tonumber(gameDataTable.Data[i][6]), tonumber(gameDataTable.Data[i][7]), tonumber(gameDataTable.Data[i][8]), tonumber(gameDataTable.Data[i][9]), tonumber(gameDataTable.Data[i][10]), tonumber(gameDataTable.Data[i][11]), gameDataTable.Data[i][12], gameDataTable.Data[i][13], gameDataTable.Data[i][14], gameDataTable.Data[i][15], gameDataTable.Data[i][16], gameDataTable.Data[i][17], tonumber(gameDataTable.Data[i][18]), tonumber(gameDataTable.Data[i][19]) );
    end

end

function MonsterDBModel.GetList()
    return monsterTable;
end

function MonsterDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #monsterTable, 1 do
        if (monsterTable[i].Id == id) then
            ret = monsterTable[i];
            break;
        end
    end
    return ret;
end