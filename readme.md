Тестовое задание на ASP.NET Core (.NET 6):

Реализовать веб-службу REST API. В качестве БД использовать PostreSQL + EntityFramework Есть несколько сущностей:

1. Блок обуривания (DrillBlock), поля: Id, Name, UpdateDate

2. Скважина (Hole), поля: Id, Name, DrillBlockId, DEPTH

3. Точки блока (DrillBlockPoints), соединяются последовательно, являются географическим контуром блока обуривания. Поля: Id, DrillBlockId, Sequence, X, Y, Z

4. Точки скважин (HolePoints) - координаты скважин. Поля: Id, HoleId, X, Y, Z

Реализовать CRUD для перечисленных сущностей. Данные передаются в формате JSON.