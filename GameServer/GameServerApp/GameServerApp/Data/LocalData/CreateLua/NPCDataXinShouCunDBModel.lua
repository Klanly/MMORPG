require "Download/XLuaLogic/Data/Create/NPCDataXinShouCunEntity"

--数据访问
NPCDataXinShouCunDBModel = { }

local this = NPCDataXinShouCunDBModel;

local npcdataxinshoucunTable = { }; --定义表格

function NPCDataXinShouCunDBModel.New()
    return this;
end

function NPCDataXinShouCunDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("NPCDataXinShouCun.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        npcdataxinshoucunTable[#npcdataxinshoucunTable+1] = NPCDataXinShouCunEntity.New( gameDataTable.Data[i][1], gameDataTable.Data[i][2], tonumber(gameDataTable.Data[i][3]), gameDataTable.Data[i][4], gameDataTable.Data[i][5], gameDataTable.Data[i][6], gameDataTable.Data[i][7] );
    end

end

function NPCDataXinShouCunDBModel.GetList()
    return npcdataxinshoucunTable;
end

function NPCDataXinShouCunDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #npcdataxinshoucunTable, 1 do
        if (npcdataxinshoucunTable[i].Id == id) then
            ret = npcdataxinshoucunTable[i];
            break;
        end
    end
    return ret;
end