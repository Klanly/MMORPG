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
        chapterlevelTable[#chapterlevelTable+1] = ChapterLevelEntity.New( tonumber(gameDataTable.Data[i][1]), gameDataTable.Data[i][2], tonumber(gameDataTable.Data[i][3]), gameDataTable.Data[i][4], gameDataTable.Data[i][5], tonumber(gameDataTable.Data[i][6]), tonumber(gameDataTable.Data[i][7]), tonumber(gameDataTable.Data[i][8]), tonumber(gameDataTable.Data[i][9]), gameDataTable.Data[i][10] );
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