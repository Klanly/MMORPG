require "Download/XLuaLogic/Data/Create/NPCDataChangAnEntity"

--数据访问
NPCDataChangAnDBModel = { }

local this = NPCDataChangAnDBModel;

local npcdatachanganTable = { }; --定义表格

function NPCDataChangAnDBModel.New()
    return this;
end

function NPCDataChangAnDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("NPCDataChangAn.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        npcdatachanganTable[#npcdatachanganTable+1] = NPCDataChangAnEntity.New( gameDataTable.Data[i][1], gameDataTable.Data[i][2], tonumber(gameDataTable.Data[i][3]), gameDataTable.Data[i][4], gameDataTable.Data[i][5], gameDataTable.Data[i][6], gameDataTable.Data[i][7] );
    end

end

function NPCDataChangAnDBModel.GetList()
    return npcdatachanganTable;
end

function NPCDataChangAnDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #npcdatachanganTable, 1 do
        if (npcdatachanganTable[i].Id == id) then
            ret = npcdatachanganTable[i];
            break;
        end
    end
    return ret;
end