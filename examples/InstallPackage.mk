PKG_MANAGER ?= # list items:dnf,flatpak name:"Пакетный менеджер"
UPGRADE ?= # switch name:"Обновить перед установкой"
PACKAGE ?= # name:"Пакет"
PASSWORD ?= # secret name:"Пароль пользователя"

all:
    if [[ $(UPGRADE) -eq "true" ]]; then; \
        echo $(PASSWORD) | sudo -S $(PKG_MANAGER) upgrade; \
    fi;
    echo $(PASSWORD) | sudo -S $(PKG_MANAGER) $(PACKAGE)