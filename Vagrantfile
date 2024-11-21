Vagrant.configure("2") do |config|
  # Вибір образу для Ubuntu (Linux)
  config.vm.box = "ubuntu/focal64" # Оновлено до Ubuntu 20.04 для сумісності з .NET SDK 8.0

  # Налаштування доступу до приватного репозиторію (за необхідності)
  config.vm.network "forwarded_port", guest: 5000, host: 5000

  # Прив'язка локальної директорії до віртуальної машини
  config.vm.synced_folder ".", "/home/vagrant/lab4"

  # Provisioning для встановлення .NET SDK і запуску програми
  config.vm.provision "shell", inline: <<-SHELL
    # Оновлення пакетів
    sudo apt update && sudo apt upgrade -y

    # Встановлення залежностей для .NET
    sudo apt install -y wget apt-transport-https software-properties-common

    # Додавання репозиторію Microsoft
    wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb

    # Оновлення та встановлення .NET SDK 8.0
    sudo apt update && sudo apt install -y dotnet-sdk-8.0

    # Перехід до робочої директорії
    cd /home/vagrant/lab4

    # Відновлення проекту
    dotnet restore

    # Побудова проекту
    dotnet build

    # Створення NuGet пакету
    dotnet pack -c Release
  SHELL
end
