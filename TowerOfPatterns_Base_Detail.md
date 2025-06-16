# Tower of Patterns — TỔNG HỢP BASE GAME

## 🎯 MỤC TIÊU TÀI LIỆU
Giúp các thành viên mới đọc xong nắm được:
- Base hiện tại có gì, tổ chức ra sao
- Các file, class chính dùng để làm gì
- Khi mở rộng, nên bắt đầu từ đâu

Tài liệu đơn giản, dễ hiểu, ít thuật ngữ chuyên sâu

---

## 📂 1️⃣ TỔ CHỨC FOLDER PROJECT

```
Assets/
_Scenes/               Scene chính của game (MainScene.unity)
_Game/
  Towers/              Logic tháp + bullet + strategy + decorator
  Enemies/             Logic enemy + factory + state
  Waves/               WaveManager, WaveBuilder
  UI/                  Quản lý giao diện
  Managers/            GameManager, GoldManager
  Common/              Singleton base, helper dùng chung
_Prefabs/              Prefab của tháp, enemy, UI
_ScriptableObjects/    (Hiện không dùng vì wave dynamic)
_Art/                  Asset hình ảnh
_Audio/                Âm thanh
```

---

## 🏗 2️⃣ TỔNG HỢP BASE THEO MODULE

### 🏯 TOWER MODULE
- `TowerBase.cs`: Tháp tìm enemy trong tầm, bắn bằng Strategy
- `ITowerAttackStrategy.cs`: Interface định nghĩa kiểu bắn
- `NormalAttackStrategy.cs`, `AoEAttackStrategy.cs`: Kiểu bắn mẫu
- `TowerFactory.cs`: Tạo tháp
- `BulletBase.cs`: Đạn bắn, gây damage + gọi effect
- `IBulletEffect.cs`: Interface decorator effect
- `BurnEffect.cs`, `SlowEffect.cs`, `PoisonEffect.cs`: Các hiệu ứng mẫu  
**Pattern:** Strategy, Decorator, Factory Method

---

### 👾 ENEMY MODULE
- `EnemyBase.cs`: Di chuyển, nhận damage, tấn công bằng Strategy
- `IEnemyAttackStrategy.cs`: Interface định nghĩa kiểu tấn công
- `MeleeAttackStrategy.cs`, `RangedAttackStrategy.cs`: Attack mẫu
- `EnemyFactory.cs`: Tạo enemy  
**Pattern:** Strategy, Factory Method  
**Lưu ý:** State hiện dùng enum + switch, chưa phải State Pattern

---

### 🌊 WAVE MODULE
- `WaveManager.cs`: Quản lý vòng lặp wave, gọi spawn
- `WaveBuilder.cs`: Sinh wave runtime (endless)
- `WaveSpawnInfo.cs`: Dữ liệu enemy trong wave  
**Pattern:** Builder

---

### 🖥 UI MODULE
- `UIManager.cs`: Update UI khi nhận event từ manager  
**Pattern:** Observer

---

### 🎮 MANAGER MODULE
- `GameManager.cs`: Quản lý máu base
- `GoldManager.cs`: Quản lý vàng, thông báo vàng thay đổi  
**Pattern:** Singleton, Observer

---

## 🧠 3️⃣ KHI MỞ RỘNG, NÊN BẮT ĐẦU TỪ ĐÂU

### TOWER
- Thêm Strategy mới cho bắn → tạo class mới kế thừa `ITowerAttackStrategy`
- Thêm Decorator mới cho hiệu ứng → tạo class kế thừa `IBulletEffect`

### ENEMY
- Thêm Strategy attack mới → tạo class mới cho kiểu tấn công
- Refactor sang State Pattern thật → tạo các class `MoveState`, `AttackState`, `DeadState`

### WAVE
- Sửa `WaveBuilder` sinh wave theo luật mới
- Thêm boss wave → thêm điều kiện đặc biệt trong Builder

### UI
- Hook thêm observer từ các manager
- Update UI text, progress bar

### OPTIMIZE
- Viết ObjectPool (Pooling) cho đạn, enemy
- Viết Command Pattern cho undo đặt/bán tháp

---

## ✨ 4️⃣ TÓM GỌN

Base hiện tại:
- Đủ chắc cho nhóm phát triển tower defense core
- Dễ mở rộng theo Design Pattern
- Code chia module rõ ràng

Nên bổ sung sớm:
- ObjectPool
- Command Pattern cho thao tác tower
- GameState Controller (flow toàn game)
- State Pattern thật cho Enemy

---

## 📌 Lưu ý cho các bạn mới:
- Mỗi tính năng chỉ làm trong module của mình, không chỉnh module khác.
- Logic mới nên tách thành class riêng (tuân theo pattern).
- Đọc kỹ comment, hỏi Leader trước khi chỉnh code gốc.
