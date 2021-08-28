<template>
<div class="tabs">
    <ul class="tabs-header">
            <li class="nav-item" v-for="title in tabTitles" 
            :key="title" 
            :class="{selected: title == selectedTitle }"
      >
            <a :href="'#' + title" @click="selectedTitle = title">
                {{ title }}
            </a>
            </li>
        </ul>
        <slot/>
</div>
</template>

<script>
import { ref, provide } from 'vue'
export default {
    props: ['selec'],
    setup(props, { slots }) {
        const tabTitles = ref(slots.default().map((tab) => tab.props.title)) 

        let selectedTitle = ref(tabTitles.value[0])
        if(ref(props.selec).value.length>0)
            selectedTitle = ref(props.selec).value;

        provide("selectedTitle", selectedTitle)
        return {
            selectedTitle,
            tabTitles
        }
    },
}
</script>

<style scoped>

.tabs-header {
    margin-bottom: 10px;
    list-style: none;
    padding: 0;
    display: flex;
}

.tabs-header a {
    text-decoration: none;
    color: #000;
}

.tabs-header li {
    text-align: center;
    padding: 10px 20px;
    margin-right: 10px;
    background-color: #ddd;
    border-radius: 5px;
    cursor: pointer;
    transition: 0.4s all ease-out;
}

.tabs-header li.selected, .tabs-header li.selected a {
    background-color: #0984e3;
    color: white;
    text-decoration: none;
    transition: 0.4s all ease-out;
}
</style>