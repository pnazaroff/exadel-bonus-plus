pipeline {
  agent {
    node {
      label 'worker'
    }

  }
  stages {
    stage('download') {
      steps {
        git(url: 'https://github.com/pnazaroff/exadel-bonus-plus', branch: 'master')
        sh 'sudo docker build -t backend .'
        sh 'docker run --name backend -d -p 5000:80 backend'
      }
    }

  }
}