# 🍳 Cooked!

A highscore *cooking game built in Unity*, where players race against time to prepare and serve as many dishes as possible!  

I built this project following code monkey's unity course to improve my grasp of the foundations when creating in Unity! 

---

## 🎮 Gameplay

In *Cooked!*, you manage a small kitchen.  
Each round challenges your speed, memory, and multitasking skills — the faster you serve, the higher your score.

*Core loop:*
1. Receive a random order 🧾  
2. Pick up ingredients 🥦🥩🍞  
3. Cook and assemble the dish 👨‍🍳  
4. Deliver before the timer runs out ⏰  
5. Earn bonus time with each completed order 🎯

---


## 🧩 Code Architecture & Design

*Cooked* built with *clean, maintainable code* and proper *software engineering practices* for scalability and readability following Code Monkey Unity Foundational Course.

### 🧠 Core Architecture

- *Modular Component-Based Design* – Each system (orders, cooking, scoring, UI) is separated into reusable modules.  
- *State Machine Pattern* – Handles player states (Idle, Picking, Cooking, Serving) and game states (Menu, Playing, GameOver) efficiently.  
- *Interfaces & Abstraction* – Used to define flexible behavior for interactable objects (IInteractable, IPickable, etc.).  
- *Event-Driven Programming* – Uses C# events and UnityActions to decouple systems (e.g., score updates, order events).  
- *Dependency Injection (where suitable)* – Reduces tight coupling between managers and systems.  
- *ScriptableObjects for Data* – Recipes, ingredients, and sounds are all data-driven for easy tuning without code changes.  
- *Singletons (limited and safe use)* – For managing core systems like GameManager, SoundManager, and Customer Manager.

---

### ⚙ Programming Practices

- *SOLID principles* followed for class design.  
- *Prefab-driven scene setup* to reduce hard-coded references.  
- *Inspector organization* with [Header], [SerializeField], and [Tooltip] attributes.  
- *Consistent version control workflow* (Git & GitHub).  

---

## 🛠 Built With

- *Unity Engine* – Core development  
- *C#* – Game logic and OOP design  
- *Audacity* – Sound editing  
- *Photoshop* – UI and asset design
- *Blender* - 3d Assets

---

## 📈 High Score System

Scores are stored locally — compete against your own best runs!

---

## 🎮 Download and Play

You can download and play the game from **Itch.io**:

👉 [**Cooked – Download on Itch.io**](https://najeeullah1.itch.io/cooked)

---
## 🚀 Run From Source (Developers)

1. Clone the repository  
   ```bash
   git clone https://github.com/Najee-Ullah/Cooked.git
