
<template>
  <div id="app">
    <BooksComponent />
  </div>
</template>

<script>
import BooksComponent from './components/BooksComponent.vue';
export default {
  name: 'App',
  components: { BooksComponent }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
