import {Button, Form, Modal} from "react-bootstrap";
import {useState} from "react";

interface ISelectFileDialog {
    show: boolean,
    onClose: () => void,
    onSelect: (file: File) => void
}

export const SelectFileDialog = ({show, onClose, onSelect}: ISelectFileDialog) => {
    const [error, setError] = useState<string>('')
    const [file, setFile] = useState<File | null>(null)

    const handleSelect = async (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files && event.target.files[0]

        if (file && file.type === '.xlsx') {
            setFile(file)
        } else {
            setError('Некорректный файл')
        }
    }

    const handleSumbit = () => {
        if (file) {
            onSelect(file)
            onClose()
        } else {
            setError('Выберите файл')
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
            <Modal.Footer>
                <Button onClick={handleSumbit} className='mx-auto'>
                    Ок
                </Button>
            </Modal.Footer>
        </Modal>
    )
}