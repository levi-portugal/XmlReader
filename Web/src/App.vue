<template>
  <div class="painel">
    <h1>XmlReader</h1>

    <hr />
    <div class="bloco">
      <GetXmlById />
    </div>

    <div class="bloco">
      <CreateXml />
    </div>


    <div class="bloco">
      <FilterXml />
    </div>

    <hr />

    <p v-if="carregando">Carregando...</p>
    <p v-if="erro" style="color: red">{{ erro }}</p>

    <p>Tudo pronto pa tu Jony 4.0</p>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'
import GetXmlById from './components/GetXmlById.vue'
import CreateXml from './components/CreateXml.vue'
import FilterXml from './components/FilterXml.vue'

const erro = ref(null)
const carregando = ref(false)

const API_URL = 'https://localhost:7058/api/xml'

</script>

<style>
.painel {
  h1 {
    text-align: center;
    color: black;
  }
  max-width: 100%;
  margin: auto;
  font-family: Arial;
  background-color: #F0F2F5;
  
  p{
    color: #212121;
  }
}

.bloco {
  margin-bottom: 20px;
}

input {
  width: 30%;
    height: 45px;
    padding: 0 16px;
    font-size: 14px;
    border: 1px solid #d1d5db;
    border-radius: 8px; /* Cantos arredondados */
    background-color: #ffffff;
    color: #212121;
    transition: all 0.2s ease-in-out;
    box-sizing: border-box;
    padding-bottom: 0px;
}

.input-field:focus {
    outline: none;
    border-color: #3f51b5;
    box-shadow: 0 0 0 3px rgba(63, 81, 181, 0.1);
}

button {
  background-color: #687df1; 
  color: #212121;
  padding: 12px 24px;
  border: none;
  border-radius: 6px; 
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

h2{
  padding-left: 10px;
}

html{
  background-color: #F0F2F5;
}
</style>