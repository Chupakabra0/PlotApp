# Гайд пользователя по PlotApp

## Инструменты разработки

Программа разработана при помощи языка программирования C# на платформе .NET 5.0 с использованием фреймворка [WPF](https://ru.wikipedia.org/wiki/Windows_Presentation_Foundation).

Файлы с точками реализован в формате [JSON](https://ru.wikipedia.org/wiki/JSON).

Остальные библиотеки:
* Newtonsoft.Json — библиотека для работы с `.json`-файлами средствами языка программирования C#.
* OxyPlot.WPF — библиотека для построения графиков на платформе .NET, ориентирована на WPF приложения.
* Microsoft.Xaml.Behaviors.WPF — библиотека для лёгкого добавления интерактивных элементов управления в WPF.
* PropertyChanged.Fody — библиотека, добавляющая уведомление об изменении свойств во всех классах, реализующих интерфейс `INotifyPropertyChanged`.
* MathParser.org-mXparser — библиотека, предоставляющая парсер строчных математических выражений.

## Ультимативный гайд по открытию проекта и его компилции

1. Открыть приложение `Visual Studio Installer` (подойдёт версия 2019-го или 2022-го годов).
2. Убедиться, что стоит галочка, как на скриншоте:
   ![Pasted image 20220622135855](https://user-images.githubusercontent.com/47476146/175049478-31a7b999-024d-41ca-9dda-eeef3de702e1.png)
3. Перейти в пункт "Отдельные компоненты":
   ![Pasted image 20220622140036](https://user-images.githubusercontent.com/47476146/175049726-bbfd09ed-5c9f-49ca-812c-f33b2332ea15.png)
4. Убедиться, что **хотя бы одна из галочек** стоит на этих пунтах:
   ![Pasted image 20220622140503](https://user-images.githubusercontent.com/47476146/175049979-8a5fff25-f80c-4ae7-a381-8078e8b8382a.png)
5. Теперь `.sln` файл проекта можно корректно открыть и скомпилировать. Для демонстрации рекомендую поставить конфигурацию решения на `Release` и скомпилировать проект (стандартная комбинация `Crtl+Shift+B`):
   ![Pasted image 20220622140746](https://user-images.githubusercontent.com/47476146/175050013-fc4f48d6-91cd-4a36-9ae0-d22b875c050d.png)
6. Если всё прошло успешно, то будет выведено сообщение, как на скриншоте:
   ![Pasted image 20220622141041](https://user-images.githubusercontent.com/47476146/175050044-55ec4c71-60bd-4941-8f4a-716e2c61bc03.png)
7. Далее нужно найти исполняемый файл программы. Следует нажать ПКМ на проект, после чего выбрать пункт "Открыть папку в проводнике":
   
   |                                      |                                      |
   | ------------------------------------ | ------------------------------------ |
   | ![Pasted image 20220622141259](https://user-images.githubusercontent.com/47476146/175050075-69a94fa2-cd2c-4f1a-bbdf-aacc5f4e7ef0.png)
 | ![Pasted image 20220622141349](https://user-images.githubusercontent.com/47476146/175050116-6859852d-4739-489d-b04a-af0975682669.png)
 |
   
   
8. Будет открыть стандартный проводник, далее нужно перейти в папку `bin`, затем `Release`, затем папку `net5.0-windows`, после чего будет найден `.exe`-файл приложения `PlotApp.exe`:
   ![Pasted image 20220622141517](https://user-images.githubusercontent.com/47476146/175050155-292ece75-f780-46aa-ab4b-8b8e2fde0654.png)

## Гайд по самому приложению

При открытии приложения нас встречает двухмерная декартова система координат и кнопка "Add".

![Pasted image 20220622141847](https://user-images.githubusercontent.com/47476146/175050188-ccd3e2bf-229f-4cde-bb75-26783e835fa5.png)

Для добавления графика функции нажмём на кнопку "Add". Появится новое окно, которое сделает предыдущее окно недоступным. На этом окне расположены несколько элементов:

![Pasted image 20220622142144](https://user-images.githubusercontent.com/47476146/175050205-fac62b25-bc1f-473d-839c-e88680ca4dd0.png)

Описание элементов управления:

<span style="color: red">1.</span> Таблица точек графика (DataGrid). \
<span style="color: lightgreen">2.</span> TextBox с именем графика. \
<span style="color: lightblue">3.</span> Slider, значение которого ограничено в рамках [0, 1] и определяет параметр ["каноничного сплайна"](https://en.wikipedia.org/wiki/Cubic_Hermite_spline#Cardinal_spline). \
<span style="color: orange">4.</span> TextBox-ы, определяющие параметры растяжения (при параметрах (1, ...]) или сжатия (при параметрах (0, 1)) по осях абсцис и ординат. \
<span style="color: purple">5.</span> TextBox-ы, определяющие параметры паралельного переноса по осям абсцис и ординат. \
<span style="color: pink">6.</span> Button-ы, для работы с файлами точек. \
<span style="color: darkgrey">7.</span> Button-ы, для завершения работы окна с сохранением графика или без.

На данный момент реализована только возможность ручного ввода точек графика функции в таблицу напрямую. Примеры работы:

|                                      |                                      |
| ------------------------------------ | ------------------------------------ |
| ![Pasted image 20220622143836](https://user-images.githubusercontent.com/47476146/175050346-ec43a42c-20cf-4f65-a7a4-4514f43a81e0.png) | ![Pasted image 20220622143847](https://user-images.githubusercontent.com/47476146/175050388-ea6f0764-5349-4a4c-bf70-24db24ae5300.png) |
| ![Pasted image 20220622144304](https://user-images.githubusercontent.com/47476146/175050422-eb0edd29-1598-48c0-bbcb-f794bcd745f4.png) | ![Pasted image 20220622144323](https://user-images.githubusercontent.com/47476146/175050447-093fc2d1-741b-4c74-91bc-15c50fdd20bc.png) |

## Проверка работоспособности фич

### Проверка работы интерполяции

Координаты графика:

![Pasted image 20220622144609](https://user-images.githubusercontent.com/47476146/175050522-8957c9af-d89d-4969-810c-d593948157df.png)

Результаты:

|               $c = 0$                |               $c=0.25$               |               $c=0.5$                |               $c=1.0$                |
|:------------------------------------:|:------------------------------------:|:------------------------------------:|:------------------------------------:|
| ![Pasted image 20220622144834](https://user-images.githubusercontent.com/47476146/175050720-72c282c5-c029-4ae9-9d4f-1ffb84eba92e.png) | ![Pasted image 20220622144845](https://user-images.githubusercontent.com/47476146/175050799-0a62d7fb-a3e0-49ba-b32f-1af164d7a8ce.png) | ![Pasted image 20220622144856](https://user-images.githubusercontent.com/47476146/175050847-f514c4b4-e2c2-4346-afdf-44a3919deecd.png) | ![Pasted image 20220622144904](https://user-images.githubusercontent.com/47476146/175050891-1fe444d4-37e2-4792-bd3f-dad086eb05f7.png) |

### Проверка растяжения/сужения

Координаты графика:

![Pasted image 20220622145128](https://user-images.githubusercontent.com/47476146/175050947-5632c4f0-61fc-4f36-8afd-ef590189dce1.png)

Результаты для оси абсцис:

|           $\alpha_{x} = 1$           |           $\alpha_{x} = 2$            |          $\alpha_{x} = 2.5$          |          $\alpha_{x} = 0.5$          |
|:------------------------------------:|:------------------------------------:|:------------------------------------:|:------------------------------------:|
| ![Pasted image 20220622145319](https://user-images.githubusercontent.com/47476146/175050989-d0847905-c1a6-45f8-839a-425b0e2eeb67.png) | ![Pasted image 20220622145335](https://user-images.githubusercontent.com/47476146/175051036-b372795a-8510-4f4a-8b5b-350f87d8fa55.png) | ![Pasted image 20220622145403](https://user-images.githubusercontent.com/47476146/175051078-4c148044-36aa-4097-9e58-cdcb2d957cf8.png) | ![Pasted image 20220622145417](https://user-images.githubusercontent.com/47476146/175051121-a45673e0-b74d-45ad-b6ec-f8488a360cb6.png) |

Результаты для оси ординат: 

|           $\alpha_{y} = 1$           |           $\alpha_{y} = 2$           |          $\alpha_{y} = 2.5$          |          $\alpha_{y} = 0.5$          |
|:------------------------------------:|:------------------------------------:|:------------------------------------:|:------------------------------------:|
| ![Pasted image 20220622145535](https://user-images.githubusercontent.com/47476146/175051304-8370f422-1aeb-489e-9746-65eee63d2c0b.png) | ![Pasted image 20220622145555](https://user-images.githubusercontent.com/47476146/175051339-f88b20f5-57de-4d66-9cfb-559d009ec8dc.png) | ![Pasted image 20220622145618](https://user-images.githubusercontent.com/47476146/175051369-eab24b7a-1665-4cdf-ab71-e3c40ab6e5ec.png) | ![Pasted image 20220622145639](https://user-images.githubusercontent.com/47476146/175051413-9c22e3a2-ea96-4b03-aa44-b25c8ce79374.png) |

### Проверка параллельного переноса

Координаты графика:
![Pasted image 20220622153549](https://user-images.githubusercontent.com/47476146/175051453-2eafa90b-c577-4fb6-9ef9-c82581c18d7f.png)

Результаты для оси абсцис:

|           $\beta_{x} = 0$            |           $\beta_{x} = 10$           |          $\beta_{x} = -10$           |
|:------------------------------------:|:------------------------------------:|:------------------------------------:|
| ![Pasted image 20220622153624](https://user-images.githubusercontent.com/47476146/175051496-42bdc189-cba6-44b3-b714-1257c20602e0.png) | ![Pasted image 20220622153648](https://user-images.githubusercontent.com/47476146/175051540-d07bc886-db1b-4580-a595-c9c65fad1a4e.png) | ![[Pasted image 20220622153734.png]] |

Результаты для оси ординат:

|           $\beta_{y} = 0$            |           $\beta_{y} = 10$            |          $\beta_{y} = -10$           |
|:------------------------------------:|:------------------------------------:|:------------------------------------:|
| ![Pasted image 20220622153627](https://user-images.githubusercontent.com/47476146/175051642-f1213d8c-74de-4f4e-8130-56d2c3d65b1d.png) | ![Pasted image 20220622153750](https://user-images.githubusercontent.com/47476146/175051696-b820a873-95a0-4ad4-a9a5-4c1eab5cb1bf.png) | ![Pasted image 20220622153805](https://user-images.githubusercontent.com/47476146/175051730-a280adb6-2595-4cb9-930f-631c208a6a4b.png) |


### Проверка редактирования/удаления

После создания первого графика появится список графиков с двумя кнопками: "Edit" и "Del", с помощью которых соответственно можно редактировать существующий график и удалять его.

Создадим несколько графиков:

![Pasted image 20220622154431](https://user-images.githubusercontent.com/47476146/175051942-b4cba5d3-0275-4199-9995-a2d8833da5cd.png)

Попробуем удалить график "Plot #1", нажав на кнопку "Del":

|                                      |                                      |
| ------------------------------------ | ------------------------------------ |
| ![Pasted image 20220622154614](https://user-images.githubusercontent.com/47476146/175051976-b3855632-0e26-4cb2-b59c-9bf80019484e.png) | ![Pasted image 20220622154625](https://user-images.githubusercontent.com/47476146/175052022-2e498fb1-eff5-43f7-9e83-18095040f6cc.png) |

Попробуем изменить график "Plot #2", нажав кнопку "Edit":

|                                      |                                      |                                      |
| ------------------------------------ | ------------------------------------ | ------------------------------------ |
| ![Pasted image 20220622154625](https://user-images.githubusercontent.com/47476146/175052022-2e498fb1-eff5-43f7-9e83-18095040f6cc.png) | ![Pasted image 20220622154927](https://user-images.githubusercontent.com/47476146/175052341-dd5f738e-5e66-4d9c-8de5-9e65ae36cb6d.png)| ![Pasted image 20220622154937](https://user-images.githubusercontent.com/47476146/175052396-1b63d6dd-1454-4fb2-bd29-27ab7ec83cab.png) |

### Проверка сохранения/загрузки файла

Как ранее было сказано, для сохранения точек используется файл с расширением `json`. Попробуем сохранить точки графика "Plot #3". Нажмём на кнопку "Edit", а в новом окне на кнопку "Save to file":

|                                      |                                      |
| ------------------------------------ | ------------------------------------ |
| ![Pasted image 20220622155316](https://user-images.githubusercontent.com/47476146/175052458-de9ccc5f-6434-420e-8c26-2d160671fe67.png) | ![Pasted image 20220622155352](https://user-images.githubusercontent.com/47476146/175052531-dc91c70b-f30f-4dee-9af3-5adf691ce701.png) |

Файл будет иметь следующий вид:

```json
[
  {
    "X": -3.0,
    "Y": 0.3
  },
  {
    "X": -2.0,
    "Y": 1.2
  },
  {
    "X": -1.0,
    "Y": -1.3
  },
  {
    "X": 0.0,
    "Y": 2.1
  },
  {
    "X": 1.0,
    "Y": 4.1
  },
  {
    "X": 2.0,
    "Y": -1.2
  },
  {
    "X": 3.0,
    "Y": 0.1
  }
]
```

Закроем приложение и откроем вновь. Попытаемся добавить новый график при помощи точек из файла:


|                                      |                                      |                                      |
| ------------------------------------ | ------------------------------------ | ------------------------------------ |
| ![Pasted image 20220622155739](https://user-images.githubusercontent.com/47476146/175052587-8472ed39-3f47-4df2-b14b-5ee636437026.png) | ![Pasted image 20220622155752](https://user-images.githubusercontent.com/47476146/175052623-ed7eca9b-7129-4d10-8150-24e1c0c3e090.png) | ![Pasted image 20220622155807](https://user-images.githubusercontent.com/47476146/175052653-a917bc40-0555-4d25-aed0-938fef9fdafc.png) |
| ![Pasted image 20220622155835](https://user-images.githubusercontent.com/47476146/175052686-1a4d65c3-fddd-4a04-ac7d-580a84c2f8bf.png) | ![Pasted image 20220622160231](https://user-images.githubusercontent.com/47476146/175052723-35fbb22d-de94-4628-aeec-4b0ddd56e133.png) | ![Pasted image 20220622160239](https://user-images.githubusercontent.com/47476146/175052759-e2790e12-1757-4a8e-be2f-65eab8f5cee2.png) |


## Управление графиком

ЛКМ — левая кнопка мышки \
ПКМ — правая кнопка мышки \
ЦКМ — центральная кнопка мышки

Исходный график:

![Pasted image 20220622164834](https://user-images.githubusercontent.com/47476146/175052820-7a7fb805-9019-4cfc-816e-ef59cd7c1c7f.png)

Перемещение по плоскости — нажатие ПКМ и перемещение мышки:

![Pasted image 20220622164902](https://user-images.githubusercontent.com/47476146/175052866-12ec20ed-b5b0-4265-80ee-a2239f86c190.png)
![Pasted image 20220622164909](https://user-images.githubusercontent.com/47476146/175052888-a3628b0a-3a54-4b21-b849-b82bd828f528.png)

Масштабирование — Колёсико мыши:

![Pasted image 20220622164943](https://user-images.githubusercontent.com/47476146/175052930-3e2eba84-383c-4b41-bb61-ce9bc2db16d6.png)
![Pasted image 20220622164953](https://user-images.githubusercontent.com/47476146/175052962-d8651441-a594-4e86-8e8c-e913bdf19af3.png)

Восстановление позиции и масштаба — двойное нажатие ЦКМ:

![Pasted image 20220622165119](https://user-images.githubusercontent.com/47476146/175053077-519da962-49e4-4128-b0d1-69ff97c03056.png)
![Pasted image 20220622165123](https://user-images.githubusercontent.com/47476146/175053113-4f6ae5b7-f960-44dd-8bab-5ef40b880fcf.png)

Фокус на сегменте — нажатие ЦКМ и выделение области:

![Pasted image 20220622165205](https://user-images.githubusercontent.com/47476146/175053164-cbaa8afa-5218-4c7b-a3e4-10327ea0e6ad.png)
![Pasted image 20220622165211](https://user-images.githubusercontent.com/47476146/175053202-784a3784-5763-44b9-8a5b-f724802019d4.png)

Значение точки — нажатие ЛКМ:

![Pasted image 20220622165452](https://user-images.githubusercontent.com/47476146/175053225-eae6e339-eb07-4f62-aae4-955e7a48ee12.png)

## Валидация

1. Наличие некорректно заполненных полей (пустое поле имени, буквы в TextBox и так далее) не даст создать график, нажав на кнопку "Done":
   ![Pasted image 20220622165618](https://user-images.githubusercontent.com/47476146/175053298-3784e6f1-ca6c-4fd7-9da1-ed4829cfabf1.png)
   ![Pasted image 20220622165632](https://user-images.githubusercontent.com/47476146/175053326-ca975d7b-bc96-4dd2-b354-e837a4c87c45.png)
   ![Pasted image 20220622165718](https://user-images.githubusercontent.com/47476146/175053352-bcb7a5e2-7137-4993-9778-b0a8ed20106b.png)
2. Текст в DataGrid не даст создать новую точку и редактировать другие
   ![Pasted image 20220622165905](https://user-images.githubusercontent.com/47476146/175053385-b1bb8efa-a5c7-4222-a4ba-830bd3a991c5.png)

## Что планируется сделать

1. Добавление точек по функции (уже реализовано, вырезано для превью-версии).
2. Возможность рисовать только точки, не соединяя их;
3. Иконка приложения.
4. Фикс мелких визуальных багов.
5. Улучшение валидации в DataGrid.
6. Прочее.
