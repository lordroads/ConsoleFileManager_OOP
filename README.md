<h1 align="center">Hi there, I'm <a href="https://daniilshat.ru/" target="_blank">Yurii</a> 
<img src="https://github.com/blackcater/blackcater/raw/main/images/Hi.gif" height="32"/></h1>
<h3 align="center">GeekBrains student of the course "Introduction to C#"</h3>

# Console file manager

**Консольный файловый менеджер начального уровня с использованием ООП (классы, наследование и прочее), который охватывает минимальный набор функций управления файлами.**

### C# Versions & `Official` Support

- Target platform ![target-image]
- Version ![version-image]


# Options

- Имеется конфигурационный файл в корневом каталоге программы "config.json", 
в конфигурационном файле возможна настройка вывода количества элементов на страницу,
так же там указана текущая директория и текущая выбранная страница.
- При выходе из программы сохраняется текущий прогресс. 
- При запуске программы, через командную строку, возможно передавать команды в качестве аргументов.

# Command Line Options

```
                                                 ИСПОЛЬЗУЕМЫЕ КОМАНДЫ:

 [view] - Выводит на экран указаную директорию. Если путь содержит пробелы, путь необходимо указатьв ковычках.
                                                        Пример:
                                               Command > view D:\MyFolder

 [page (p)] - Переходит на указаную страницу, если такая имеется.
                                                        Пример:
                                                    Command > page 2

 [copy (c)] - Копирует указаный файл или директория по указаному пути.
                                                        Пример:
                                        Command > copy D:\MyFolder D:\SomeFolder

 [delete (d)] - Удаляет указаный файл или директория по указаному пути.
                                                        Пример:
                                              Command > delete D:\MyFolder

 [create (cr)] - Создает указаный файл или директория по указаному пути.
                                                        Пример:
                                        Command > create D:\MyFolder\newFile.txt

 [rename (re)] - Переименовывает указаный файл или директорию.
 НЕОБХОДИМО находится в директории указываемого файла или директории.
                                                        Пример:
                                           Command > re newFile.txt README.md

 [info (i)] - Выводит на экран информацию об указаном файле или директории.
 НЕОБХОДИМО находится в директории указываемого файла или директории.
                                                        Пример:
                                           Command > re newFile.txt README.md

 [help] - Показывает какие команды есть у программы.

 [quit] - Выход из программы.

```

# Version

2.0.0 Version

[target-image]: https://img.shields.io/badge/.NET-6.0-green
[version-image]: https://img.shields.io/badge/C%23-9.0-green

# Sponsors

[Yurii Mitrokhin](https://github.com/lordroads)
