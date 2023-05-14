import {Button, Container} from "react-bootstrap";
import styles from './EditorPage.module.css';
import React from "react";
import { message } from 'antd';

export const EditorPage = () => {
    const [messageApi] = message.useMessage()

    return (
        <Container className={styles.container}>
            <Button onClick={() => messageApi.error("ggggg")}>
                SAaaasf
            </Button>
        </Container>
    )
}