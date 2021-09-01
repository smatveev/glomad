<template>
    <div class="tab-content" v-show="title == selectedTitle">
        <slot/>
    </div>
</template>

<script>
import {inject} from 'vue'

export default {
    props: ['title'],
    emits: ['click'],

    setup() {
        const selectedTitle = inject("selectedTitle")

        return {
            selectedTitle
        }
    },
}
</script>

<style scoped>
    .tab-content {
        min-height: 400px;
        padding: 20px;
        border-radius: 5px;
    }
</style>