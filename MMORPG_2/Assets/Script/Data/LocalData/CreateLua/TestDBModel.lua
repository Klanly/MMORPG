require "Download/XLuaLogic/Data/Create/TestEntity"

--数据访问
TestDBModel = { }

local this = TestDBModel;

local testTable = { }; --定义表格

function TestDBModel.New()
    return this;
end

function TestDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Test.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        testTable[#testTable+1] = TestEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], tonumber(gameDataTable.Data[i][2]), tonumber(gameDataTable.Data[i][3]), tonumber(gameDataTable.Data[i][4]), tonumber(gameDataTable.Data[i][5]), tonumber(gameDataTable.Data[i][6]) );
    end

end

function TestDBModel.GetList()
    return testTable;
end

function TestDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #testTable, 1 do
        if (testTable[i].Id == id) then
            ret = testTable[i];
            break;
        end
    end
    return ret;
end