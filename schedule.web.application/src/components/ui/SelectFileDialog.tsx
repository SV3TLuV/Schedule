import {Button, Form, Modal} from "react-bootstrap";
import {useState} from "react";

interface ISelectFileDialog {
    show: boolean,
    onClose: () => void,
    onSelect: (file: File) => void
}

export const SelectFileDialog = ({show, onClose, onSelect}: ISelectFileDialog) => {
    const [error, setError] = useState<string>('')

    const handleSelect = async (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files && event.target.files[0]

        if (file && file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet') {
            onSelect(file)
            onClose()
        } else {
            setError('Некорректный файл')
        }
    }

    return (
        <Modal
            onHide={onClose}
            show={show}
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title>
                    {'Выберите файл'}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form.Group className='text-center'>
                    <Form.Control
                        type="file"
                        onChange={handleSelect}
                        accept='.xlsx'
                    />
                    {error &&
                        <Form.Text className="text-danger">
                            {error}
                        </Form.Text>
                    }
                </Form.Group>
            </Modal.Body>
        </Modal>
    )
}