# Minesweeper — опис рішення
Зміст
- Overview
- Як запустити
- Структура проекту
- Примітка про планування
- Programming Principles
- Design Patterns
- Refactoring Techniques
- Як розширювати
- Головні файли (швидкий перелік)

Overview
Проєкт розбитий на дві частини:
- ClassLibrary1 — ядро: моделі, сервіси, сховища, DTO.
- WinFormsApp1 — UI: форми та контролери (CellButton, MainForm, GameForm).

Як запустити
1. Відкрийте рішення у Visual Studio 2022.
2. Зберіть рішення (Build Solution).
3. Запустіть проект WinFormsApp1 як стартовий проект.
(Налаштування звичайні для WinForms; якщо потрібно — змінити стартовий проект у Solution Explorer.)

Примітка про планування
2. Ви можете додавати функціонал як на початку, так і вкінці створення проєкту. Але для себе бажано все продумати наперед. Планування — найважливіший етап написання коду. Рекомендується визначити відповідальності модулів, інтерфейси та точки розширення перед внесенням великих змін.

Programming Principles
Нижче — 5 принципів, яких дотримано в проєкті.

1. Single Responsibility Principle (SRP)
   - Кожен клас має одну відповідальність: GameBoard — структура поля, GameLogicService — правила гри, JsonSettingsRepository/JsonStatisticsRepository — збереження/завантаження.
   - Файли: ClassLibrary1\Models\GameBoard.cs, ClassLibrary1\Services\GameLogicService.cs, ClassLibrary1\Data\JsonSettingsRepository.cs

2. Open/Closed Principle (OCP)
   - Розширюваність через інтерфейси: щоб змінити генератор або сховище, не потрібно змінювати споживачів, достатньо додати нову реалізацію.
   - Файли: ClassLibrary1\Interfaces\IGameBoardGenerator.cs, WinFormsApp1\Program.cs

3. Liskov Substitution Principle (LSP)
   - Інтерфейси та реалізації взаємозамінні (наприклад, реалізації ISettingsRepository / IStatisticsRepository можна замінити моками для тестів).

4. Interface Segregation Principle (ISP)
   - Вузькі, сфокусовані інтерфейси (IGameBoardGenerator, IGameLogicService, репозиторії) — клієнти не змушені залежати від зайвих членів.

5. DRY (Don't Repeat Yourself)
   - Повторювану логіку виділено у методи/класі (DTO mapping, частини генератора поля), щоб уникнути дублікацій.
   - Приклади: ClassLibrary1\Data\SettingsDto.cs, ClassLibrary1\Data\StatisticsDto.cs, GameBoardGenerator.GetAllPositions / Shuffle.

Design Patterns
У коді застосовано такі види патернів проєктування.

1. Dependency Injection (контейнер ручної ін’єкції)
   - Де: WinFormsApp1\Program.cs, GameLogicService конструктор.
   - Чому: Впровадження залежностей (IGameBoardGenerator, репозиторії) дозволяє замінювати реалізації без зміни споживача, полегшує тестування.

2. Repository Pattern
   - Де: ClassLibrary1\Data\JsonSettingsRepository.cs, ClassLibrary1\Data\JsonStatisticsRepository.cs (реалізації) та відповідні інтерфейси (ISettingsRepository, IStatisticsRepository).
   - Чому: Відокремлює механіку збереження/завантаження від доменної логіки, дозволяє легко змінити формат збереження (файл → база даних) без змін у логіці гри.

3. Strategy / Pluggable Algorithm
   - Де: ClassLibrary1\Interfaces\IGameBoardGenerator.cs та ClassLibrary1\Services\GameBoardGenerator.cs.
   - Чому: Алгоритм генерації мін інкапсульований в окремий клас (стратегія). Можна додати інші генератори (детермінований, для тестування) та підміняти їх у Program.cs.

4. Factory Method (іменовані фабричні методи)
   - Де: ClassLibrary1\Models\GameSettings.cs (Easy(), Medium(), Hard()).
   - Чому: Забезпечує однозначні, читабельні способи створення налаштувань гри замість розкиданих літералів.

5. Data Transfer Object (DTO)
   - Де: ClassLibrary1\Data\SettingsDto.cs, ClassLibrary1\Data\StatisticsDto.cs
   - Чому: DTO відповідають за формат серіалізації/десеріалізації JSON і відділяють формат збереження від доменної моделі.

Refactoring Techniques
Перелік технік, які застосовано під час розробки.

1. Extract Method
   - Приклад: ClassLibrary1\Services\GameBoardGenerator.cs — CalculateAdjacentMineCounts, GetAllPositions, Shuffle виділені в окремі методи для підвищення читабельності.

2. Introduce Guard Clauses (Null checks / Argument validation)
   - Приклад: ArgumentNullException.ThrowIfNull(...) у DTO та сервісах; перевірки аргументів у конструкторах (GameBoard, GameSettings, Cell).

3. Extract Class / Separate Concerns
   - Приклад: Винос відповідальності з моделі (серіалізація) у JsonSettingsRepository / JsonStatisticsRepository та DTO.

4. Replace Magic Numbers with Named Factories / Constants
   - Приклад: GameSettings.Easy/Medium/Hard замінюють «магічні» числа розмірів і кількості мін.

5. Encapsulate Field / Encapsulate State
   - Приклад: ClassLibrary1\Models\Cell.cs — відкриті дані керуються через методи (Reveal, ToggleFlag, PlaceMine) замість прямого маніпулювання ззовні.

6. Introduce DTO
   - Приклад: SettingsDto/StatisticsDto як проміжний шар для серіалізації.

7. Simplify Conditional Expressions
   - Приклад: У GameLogicService та GameStatistics використані зрозумілі switch/if-блоки й ранні виходи для зменшення вкладеності.

Головні файли
- Domain / Logic:
  - ClassLibrary1\Models\Cell.cs
  - ClassLibrary1\Models\GameBoard.cs
  - ClassLibrary1\Models\GameSettings.cs
  - ClassLibrary1\Models\GameStatistics.cs
  - ClassLibrary1\Services\GameLogicService.cs
  - ClassLibrary1\Services\GameBoardGenerator.cs
- Interfaces:
  - ClassLibrary1\Interfaces\IGameBoardGenerator.cs
  - ClassLibrary1\Interfaces\IGameLogicService.cs
- Persistence:
  - ClassLibrary1\Data\JsonSettingsRepository.cs
  - ClassLibrary1\Data\JsonStatisticsRepository.cs
  - ClassLibrary1\Data\SettingsDto.cs
  - ClassLibrary1\Data\StatisticsDto.cs
- UI:
  - WinFormsApp1\Program.cs
  - WinFormsApp1\MainForm.cs
  - WinFormsApp1\GameForm.cs
  - WinFormsApp1\CellButton.cs