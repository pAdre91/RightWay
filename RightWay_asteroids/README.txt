В проекте есть баг.

Если игрок долго не умерает, то вражеские корабли перестают стрелять. Происходит это из-за того, что я не инициализирую объекты при взятии из пула. На исправление не хватило времени.
Для этого моджно добавить в интерфейс IPoolable(или создать новый, более узкий интерфейс, ибо не ысем объектам нужна инициализация после взятия из пула) метод Init и вызывать его после взятия объекта из пула.