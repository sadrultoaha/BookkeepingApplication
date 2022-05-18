
<template>
  <div class="sidenav">
    <router-link class="nav-link" :to="'/'">Dashboard</router-link>
    <span><hr /></span>

    <router-link class="nav-link" :to="'/income'">Income Expense Types</router-link>
    <span><hr /></span>

    <router-link class="nav-link" :to="'/income'">Income</router-link>
    <span><hr /></span>

    <router-link class="nav-link" :to="'/expense'">Expense</router-link>
    <span><hr /></span>

    <router-link class="nav-link" :to="'/reconciliation'"
      >Recociliation</router-link
    >
    <span><hr /></span>
  </div>
</template>


<style scoped>
.sidenav {
  position: fixed;
  z-index: 1;
  overflow: auto;
  height: 100%;
  width: 250px;
  background-color: rgba(147, 196, 125, 1);
  border-style: solid;
  border-color: #212529;
  padding-left: 10px;
  padding-right: 10px;
  top: 48px;
  left: 0px;
}

.nav-link {
  padding: 6px 8px 6px 16px;
  font-size: medium;
  font-family: cursive;
  color: black;
  display: block;
  text-align: center;
}

.nav-link:hover {
  color: #f1f1f1a4;
}

.nav-link:link {
  text-decoration: none;
}

.nav-link:active {
  color: white;
}

.sidenav hr {
  margin-top: 0rem;
  margin-bottom: 1rem;
  margin-left: 10px;
  margin-right: 10px;
  border-top: 3px solid black;
}
@media screen and (max-width: 700px) {
  .sidenav {
    width: 100%;
    height: auto;
    position: relative;
  }
  .sidenav a, span {
    float: left;
  }
  div.content {
    margin-left: 0;
  }
}

@media screen and (max-width: 400px) {
  .sidenav a, span {
    text-align: center;
    float: none;
  }
}
</style>

