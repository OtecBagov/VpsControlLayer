# Руководство по командам CLI

## Быстрый старт

```bash
# 1. Инициализация
dotnet run -- init --server <IP> --domain <DOMAIN>

# 2. Деплой на сервер
dotnet run -- deploy

# 3. Проверка статуса
dotnet run -- status

# 4. Ротация ключей (опционально)
dotnet run -- rotate-keys
```

---

## Команда: init

**Назначение:** Создание конфигурации для VPN сервера.

**Синтаксис:**
```bash
dotnet run -- init --server <IP> --domain <DOMAIN> [--port <PORT>] [--user <USER>]
```

**Обязательные параметры:**
- `--server` - IP адрес VPS сервера
- `--domain` - Домен для маскировки (например, google.com)

**Опциональные параметры:**
- `--port` - Порт (по умолчанию: 443)
- `--user` - SSH пользователь (по умолчанию: root)

**Что делает:**
1. Генерирует x25519 ключи
2. Создает UUID клиента
3. Генерирует Short IDs
4. Сохраняет конфиг в ~/.vpnctl/server-config.json

**Пример:**
```bash
dotnet run -- init --server 45.67.89.123 --domain google.com
dotnet run -- init --server 45.67.89.123 --domain youtube.com --port 8443 --user admin
```

---

## Команда: deploy

**Назначение:** Загрузка конфигурации на сервер и запуск Xray.

**Синтаксис:**
```bash
dotnet run -- deploy
```

**Что делает:**
1. Читает конфиг из ~/.vpnctl/server-config.json
2. Подключается к серверу по SSH
3. Загружает конфиг в /usr/local/etc/xray/config.json
4. Перезапускает сервис: systemctl restart xray

**Требования:**
- Выполненная команда init
- SSH доступ к серверу
- Xray установлен на сервере

---

## Команда: status

**Назначение:** Проверка состояния VPN сервера.

**Синтаксис:**
```bash
dotnet run -- status
```

**Что показывает:**
- Статус сервиса Xray
- Количество активных клиентов
- Uptime сервера
- Задержка (ping)
- Использование трафика

**Пример вывода:**
```
Статус VPN сервера
Сервер:     45.67.89.123 (ping: 25ms)
Xray:       Активен (uptime: 3d 12h)
Клиентов:   3 / 10
Трафик:     1.2 GB / 450 MB
```

---

## Команда: rotate-keys

**Назначение:** Генерация новых ключей безопасности.

**Синтаксис:**
```bash
dotnet run -- rotate-keys
```

**Что делает:**
1. Создает резервную копию текущих ключей
2. Генерирует новую пару x25519 ключей
3. Обновляет конфигурацию

**После ротации:**
```bash
# 1. Применить на сервере
dotnet run -- deploy

# 2. ВАЖНО: Обновить клиентские конфигурации
# Старые конфиги перестанут работать!
```

**Рекомендации:**
- Выполнять раз в 1-3 месяца
- При компрометации - немедленно
- Предупреждать пользователей заранее

---

## Типичные сценарии

### Новый сервер
```bash
dotnet run -- init --server 45.67.89.123 --domain google.com
dotnet run -- deploy
dotnet run -- status
```

### Плановая ротация ключей
```bash
dotnet run -- rotate-keys
dotnet run -- deploy
dotnet run -- status
# Раздать новые клиентские конфиги
```

### Проверка работоспособности
```bash
dotnet run -- status
```

---

## Устранение проблем

### Ошибка: Конфигурация не найдена
```bash
# Решение: Выполнить init
dotnet run -- init --server <IP> --domain <DOMAIN>
```

### Ошибка: SSH подключение не удалось
```bash
# Проверить доступность
ping <server_ip>

# Проверить SSH
ssh <user>@<server_ip>
```

### Ошибка: Xray не запущен
```bash
# Проверить на сервере
ssh root@<server> 'systemctl status xray'

# Посмотреть логи
ssh root@<server> 'journalctl -u xray -n 50'

# Перезапустить
ssh root@<server> 'systemctl restart xray'
```

---

## Файлы и пути

```
~/.vpnctl/
├── server-config.json           # Текущая конфигурация
└── backups/                     # Резервные копии
    └── server-config-backup-*.json
```

---

## Справка

```bash
# Общая справка
dotnet run -- --help

# Справка по команде
dotnet run -- init --help
```