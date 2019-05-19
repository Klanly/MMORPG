require "Download/XLuaLogic/Data/Create/ChapterLevelEntity"

--数据访问
ChapterLevelDBModel = { }

local this = ChapterLevelDBModel;

local chapterlevelTable = { }; --定义表格

function ChapterLevelDBModel.New()
    return this;
end

function ChapterLevelDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("ChapterLevel.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        chapterlevelTable[#chapterlevelTable+1] = ChapterLevelEntity.New( tonumber(gameDataTable.Data[i][1]), tonumber(gameDataTable.Data[i][2]), gameDataTable.Data[i][3], tonumber(gameDataTable.Data[i][4]), tonumber(gameDataTable.Data[i][5]), tonumber(gameDataTable.Data[i][6]), tonumber(gameDataTable.Data[i][7]), gameDataTable.Data[i][8], gameDataTable.Data[i][9], tonumber(gameDataTable.Data[i][10]), tonumber(gameDataTable.Data[i][11]), tonumber(gameDataTable.Data[i][12]), tonumber(gameDataTable.Data[i][13]), gameDataTable.Data[i][14] );
    end

end

function ChapterLevelDBModel.GetList()
    return chapterlevelTable;
end

function ChapterLevelDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #chapterlevelTable, 1 do
        if (chapterlevelTable[i].Id == id) then
            ret = chapterlevelTable[i];
            break;
        end
    end
    return ret;
end